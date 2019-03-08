using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Back.Helpers;
using Back.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors;
using System.Globalization;
using StackExchange.Exceptional;
using Back.Models;


namespace Back.Controllers
{
    public class AccountController : Controller
    {

        private static readonly HttpClient client = new HttpClient();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AccountController(IConfiguration configuration,
            IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost]
        [Route("api/glogin")]
        public IActionResult GLogin()
        {
            string secret = _configuration.GetValue<string>("CLIENT_ID");
            string returnulr = _configuration.GetValue<string>("RETURN_URL");
            return Redirect(
               $@"https://accounts.google.com/o/oauth2/auth?redirect_uri={returnulr}&response_type=code&client_id={secret}&scope=https://www.googleapis.com/auth/userinfo.email&approval_prompt=force&access_type=offline");
            
        }

        [HttpGet]
        [Route("api/gloginreturn")]
        public IActionResult GLoginReturn(string code)
        {
            //Busco el token
            var request = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");

            string postData = 
                string.Format(
                    $"code={code}&client_id={_configuration.GetValue<string>("CLIENT_ID")}&client_secret={_configuration.GetValue<string>("GOOGLE_SECRET")}&redirect_uri={_configuration.GetValue<string>("RETURN_URL")}&grant_type=authorization_code");
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var result = JsonConvert.DeserializeObject<GoogleToken>(responseString);

            var request2 = (HttpWebRequest)WebRequest.Create(
                $"https://www.googleapis.com/oauth2/v2/userinfo?access_token={result.access_token}");
            request2.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            response = (HttpWebResponse)request2.GetResponse();
            responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //Ahora hay que traer los datos, y crear el objeto a devolver.
             


            return new OkResult();
        }

        [HttpGet]
        [Authorize]
        [Route("api/GetUserGroups")]
        public async Task<IActionResult> GetGroups()
        {
            int UserId = User.GetClaim<int>(ClaimType.Id);
            return new OkObjectResult(await _userService.GetUserGroups(UserId));
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> LogInAsync([FromBody] UserInfo user)
        {
            //Logueo usr/pass
            user = await _userService.LoginUserAsync(user.Name, user.Password);
            if (user == null)
            {
                return BadRequest(1);
            }
            //Genero las claims. Si pago, no pago, o si es admin!
            var claims = new List<Claim>
            {
                new Claim(ClaimType.Id, user.Id.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimType.Name, user.Name),
                new Claim(ClaimType.Mail, user.Mail),
                new Claim(ClaimType.TeamName, user.TeamName),
                new Claim(ClaimType.GameGroupId, user.GameGroupId.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimType.GoogleMail, user.GoogleMail)
            };

            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimType.IsAdmin, "1"));
            }
            if (user.HasPaid)
            {
                claims.Add(new Claim(ClaimType.HasPaid, "1"));
            }
            if (user.ReceiveMails)
            {
                claims.Add(new Claim(ClaimType.ReceiveMails, "1"));
            }
            if (user.ReceiveAdminMails)
            {
                claims.Add(new Claim(ClaimType.ReceiveAdminMails, "1"));
            }
            if (user.GoogleLogin)
            {
                claims.Add(new Claim(ClaimType.GoogleLogin, "1"));
            }
            var identity = new ClaimsIdentity(claims, "login");

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            return new OkObjectResult(true);
        }
    }
}
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

namespace Back.Controllers
{
    public class AccountController : Controller
    {

        private static readonly HttpClient client = new HttpClient();
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
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
        public void GLoginReturn(string code)
        {

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

            //https://www.googleapis.com/oauth2/v1/userinfo?access_token=' + token
            //var request2 = (HttpWebRequest)WebRequest.Create("https://content.googleapis.com/userinfo/v2/me?fields=email&key=XYZ123");
            string a1 = $"https://content.googleapis.com/userinfo/v2/me?fields=email&key={result.refresh_token}";
            string a2 = $"https://content.googleapis.com/userinfo/v2/me?fields=email&key={result.access_token}";
            string a3 = $"https://content.googleapis.com/userinfo/v2/me?fields=email&key={result.id_token}";
            //var request2 = (HttpWebRequest)WebRequest.Create(
            //    $"https://content.googleapis.com/userinfo/v2/me?fields=email&key={_configuration.GetValue<string>("CLIENT_ID")}");
            var request2 = (HttpWebRequest)WebRequest.Create(
                $"https://www.googleapis.com/oauth2/v1/userinfo?access_token={result.access_token}");
            //request2.Headers.Add("Bearer", result.access_token);
            //request2.Headers.Add("Authorization", "Bearer " + result.access_token);
            request2.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            response = (HttpWebResponse)request2.GetResponse();
            responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            //Ahora deberia hacer un get a mi?


            //client.DefaultRequestHeaders
            //    .Accept
            //    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            //var values = new Dictionary<string, string>
            //{
            //    { "Code", code },
            //    { "Client_id", _configuration.GetValue<string>("CLIENT_ID") },
            //    { "Client_secret", _configuration.GetValue<string>("GOOGLE_SECRET") },
            //    { "Redirect_uri",  _configuration.GetValue<string>("RETURN_URL")},
            //    { "grant_type", "authorization_code" }
            //};

            //var content = new FormUrlEncodedContent(values);

            //var response = await client.PostAsync(" https://accounts.google.com/o/oauth2/token", content);

            //var responseString = await response.Content.ReadAsStringAsync();
            ////Get the data
            return;
        }

    }
}
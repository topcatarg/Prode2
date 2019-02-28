using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    public class ResultsController : Controller
    {
        private readonly IResultService _resultService;
        private readonly IAuthorizationService _authorizationService;

        public ResultsController(IAuthorizationService authorizationService,
            IResultService resultService)
        {
            _resultService = resultService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Authorize]
        [Route("api/results")]
        public async Task<IActionResult> GetResults(int GroupNumber)
        {
            return new OkObjectResult(await _resultService.GetResults(GroupNumber));
        }
    }
}
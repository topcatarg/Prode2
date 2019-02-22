using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back.Services;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    public class FixtureController : Controller
    {
        private readonly IFixtureService _fixtureService;

        public FixtureController(IFixtureService fixtureService)
        {
            _fixtureService = fixtureService;
        }

        [HttpGet]
        [Route("api/fixture/allmatchs")]
        public async Task<IActionResult> AllMatchs()
        {
            return Json(await _fixtureService.GetAllMatchs());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Exceptional;

namespace Back.Controllers
{
    public class AdminController : Controller
    {


        //public async Task Exceptions() => await ExceptionalMiddleware.HandleRequestAsync(HttpContext);
        [HttpGet]
        [Route("api/admin/errors/{path?}/{subPath?}")]
        public Task Exceptions()
        {
            return ExceptionalMiddleware.HandleRequestAsync(HttpContext);
        }

    }
}
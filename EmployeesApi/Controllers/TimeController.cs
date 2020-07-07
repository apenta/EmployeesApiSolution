using EmployeesApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesApi.Controllers
{
    public class TimeController : ControllerBase
    {
        //ISystemTime clock;
        //insert constructor

        [HttpGet("time")] //GET /time

        public ActionResult GetTheTime([FromServices] ISystemTime clock)
        {
            return Ok($"The time is {clock.GetCurrent().ToLongTimeString()}");
        }
    }
}

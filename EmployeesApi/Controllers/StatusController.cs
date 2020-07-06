using EmployeesApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EmployeesApi.Controllers
{
    public class StatusController : ControllerBase
    {
        ISystemTime Time;


        public StatusController(ISystemTime time)
        {
            Time = time;
        }

        //GET / status
        [HttpGet("status")]
        public ActionResult GetStatus()
        {

            //1. TODO: Go check the actual status
            var response = new StatusResponse
            {
                Status = "This is the status.",
                CheckedBy = "Scottie",
                LastChecked = DateTime.Now.AddMinutes(-15)
            };
            return Ok(response);
        }

        public class StatusResponse
        {
            public string Status { get; set; }
            public string CheckedBy { get; set; }
            public DateTime LastChecked { get; set; }
        }

        // 1. Route Params
        [HttpGet("books/{bookId:int}")]
        public ActionResult GetABook(int bookId)
        {
            return Ok($"Getting you book info for: {bookId}");
        }
        [HttpGet("blogs/{year:int}/{month:int:range(1,12)}/{date:int}")]
        public ActionResult GetBlogPostsFor(int year, int month, int date)
        {
            return Ok($"Getting blog posts for  {month}/{date}/{year}");
        }
        // 2. Query Strings
        [HttpGet("books")]
        public ActionResult GetBooks([FromQuery] string genre = "All")
        {
            return Ok($"Getting you books in the {genre} genre");
        }

        // 3. Briefly Headers
        [HttpGet("whoami")]
        public ActionResult WhoAmI([FromHeader(Name = "User-Agent")] string userAgent)
        {
            return Ok($"I see you are running {userAgent}");
        }

        // 4. Entities
    }
}

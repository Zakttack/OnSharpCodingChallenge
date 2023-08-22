using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BowlingApplication.Models;
using BowlingLibrary.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BowlingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BowlingGameController : ControllerBase
    {
        [HttpPost]
        [Route("Bowl")]
        public IActionResult Bowl(PinsKnockedDownInput request)
        {
            try
            {
                Service.Players.ToList()[Service.CurrentIndex].Bowl(request.Value);
                return Ok();
            }
            catch (AmountKnockedDownException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
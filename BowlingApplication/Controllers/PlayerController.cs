using BowlingApplication.Models;
using BowlingLibrary;
using BowlingLibrary.Exceptions;
using Microsoft.AspNetCore.Mvc;
namespace BowlingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> logger;

        public PlayerController(ILogger<PlayerController> logger)
        {
            this.logger = logger;
        }
        [HttpPost]
        [Route("AddPlayer")]
        public IActionResult AddPlayer(PlayerNameInput request)
        {
            try
            {
                Service.Players.AddPlayer(request.Text);
                return Ok();
            }
            catch (PlayerAlreadyExistsException ex)
            {
                return StatusCode(400,ex.Message);
            }
        }

        [HttpPost]
        [Route("VerifyNonEmptyPlayers")]
        public IActionResult VerifyNonEmptyPlayers()
        {
            return Service.Players.ToList().Count > 0 ? Ok() : StatusCode(400, "No Players are Bowling.");
        }
    }
}
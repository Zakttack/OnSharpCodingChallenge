using BowlingApplication.Models;
using BowlingLibrary;
using BowlingLibrary.Exceptions;
using BowlingLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace BowlingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> logger;
        private readonly Round round;

        public PlayerController(ILogger<PlayerController> logger)
        {
            this.logger = logger;
            round = new();
        }

        [HttpPost]
        [Route("AddPlayer")]
        public IActionResult AddPlayer(PlayerRequest player)
        {
            round.AddPlayer(player.PlayerName);
            return Ok();
        }
    }
}
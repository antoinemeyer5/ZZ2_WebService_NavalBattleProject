using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NavalWar.Business;
using NavalWar.DTO;

namespace Bataille_Navale.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IGameService _gameService;

        public PlayerController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // PUT api/PlayerController/CreatePlayer
        [HttpPut("CreatePlayer")]
        public IActionResult CreatePlayer([FromForm] string name)
        {
            PlayerDTO p = _gameService.CreatePlayer(name);
            if (p != null)
            {
                return Ok(p);
            }

            return StatusCode(400);
        }


        // Delete api/PlayerController/Players/{playerId}
        [HttpDelete("Players/{playerId}")]
        public IActionResult DeletePlayer(int playerId)
        {
            if (_gameService.DeletePlayer(playerId))
                return Ok();

            return StatusCode(400); // We should not care ?
        }

        // Delete api/PlayerController/Players/{playerId}
        [HttpPost("Players/{playerId}")]
        public IActionResult UpdatePlayer(int playerId, [FromForm] string name)
        {
            PlayerDTO p = _gameService.UpdatePlayer(playerId, name);
            if (p != null)
                return Ok();

            return StatusCode(400);
        }
    }
}

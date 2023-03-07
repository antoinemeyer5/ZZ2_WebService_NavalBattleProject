using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NavalWar.Business;
using NavalWar.DAL.Repositories;
using NavalWar.DTO;

namespace Bataille_Navale.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService gameService)
        {
            _playerService = gameService;
        }

        // PUT api/PlayerController/CreatePlayer
        [HttpPut("CreatePlayer")]
        public IActionResult CreatePlayer([FromForm] string name)
        {
            PlayerDTO p = _playerService.CreatePlayer(name);
            if (p != null)
            {
                return Ok(p);
            }

            return StatusCode(400);
        }

        // GET api/GameArea/Players
        [HttpGet("Players/{id}")]
        public IActionResult GetPlayer(int id)
        {
            PlayerDTO p = _playerService.GetPlayer(id);
            if (p == null)
                return StatusCode(400);

            return Ok(p);
        }


        // Delete api/PlayerController/Players/{playerId}
        [HttpDelete("Players/{playerId}")]
        public IActionResult DeletePlayer(int playerId)
        {
            if (_playerService.DeletePlayer(playerId))
                return Ok();

            return StatusCode(400); // We should not care ?
        }

        // Delete api/PlayerController/Players/{playerId}
        [HttpPost("Players/{playerId}")]
        public IActionResult UpdatePlayer(int playerId, [FromForm] string name)
        {
            PlayerDTO p = _playerService.UpdatePlayer(playerId, name);
            if (p != null)
                return Ok();

            return StatusCode(400);
        }
    }
}

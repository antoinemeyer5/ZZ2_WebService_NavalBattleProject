using Microsoft.AspNetCore.Mvc;
using NavalWar.DTO;
using NavalWar.Business;
using NavalWar.DAL.Repositories;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bataille_Navale.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class GameAreaController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IPlayerService _playerService;
        public GameAreaController(IGameService gameService, IPlayerService playerService)
        {
            _gameService = gameService;
            _playerService = playerService;
        }

        [HttpGet("Games/{id}")]
        //Get api/Games/{id}
        public IActionResult GetGame(int id)
        {
            try
            {
                GameDTO g = _gameService.GetGameByID(id);
                return Ok(g);
            }
            catch(Exception)
            {
                return StatusCode(400);
            }  
        }


        [HttpGet("Games/{gameID}/Maps/{numPlayer}")]
        // GET api/GameArea/Games/{gameID}/Maps/{numPlayer/Map}
        public IActionResult GetPlayerMap(int gameID, int numPlayer)
        {
            if(numPlayer < 0 && numPlayer >1)// numplayer € [0; 1]
                return StatusCode(400);

            try
            {
                GameDTO g = _gameService.GetGameByID(gameID);
                return Ok(g.ListMap[numPlayer]);    
            }
            catch(Exception)
            {
                return StatusCode(400);
            }
        }

        [HttpPut("JoinGame")]
        // PUT api/GameArea/JoinGame
        public IActionResult JoinGame([FromForm] int idGame, [FromForm] int idPlayer)
        {
            try
            {
                _gameService.JoinGame(_playerService.GetPlayer(idPlayer), idGame);
                return Ok(_gameService.GetGameByID(idGame));
            }
            catch(Exception) {
                return StatusCode(400);
            }
        }

        [HttpPut("HostGame")]
        // PUT api/GameArea/HostGame
        public IActionResult HostGame([FromForm] int idPlayer)
        {
            try
            {
                GameDTO g = _gameService.HostGame(_playerService.GetPlayer(idPlayer));
                return Ok(g);
            }
            catch (Exception)
            {
                return StatusCode(400);
            }
        }



        // PUT api/GameArea/Games/{gameID}/Putship/numPlayer
        [HttpPut("Games/{gameID}/PutShip/{numPlayer}")]
        public IActionResult PutShip(int gameID, int numPlayer, [FromForm] int numShip, [FromForm] int line, [FromForm] int column, [FromForm] Orientation orientation)
        {
            if(numPlayer< 0 || numPlayer > 1)
                return StatusCode(400);

            GameDTO g = _gameService.GetGameByID(gameID);
            if (g == null)
                return StatusCode(400);

            int i = (numPlayer==0) ? 1 : 0;
            MapDTO m = g.ListMap[i];
            if (m == null)
                return StatusCode(400);
            try
            {
                m.PlaceShip(numShip, line, column, orientation);
                bool res = _gameService.PutShip(gameID, numPlayer, numShip, line, column, orientation);
                
                if (res) return Ok();
            }
            catch(Exception)
            {
                
            }

            return StatusCode(400);
        }

        [HttpPut("Games/{gameID}/Target/{numPlayer}")]
        // PUT api/GameArea/Games/{gameID}/Target/numPlayer
        public IActionResult PutTarget(int gameID, int numPlayer, [FromForm] int line, [FromForm] int column)
        {
            int res;
            try
            {
                res = _gameService.Target(gameID, numPlayer, line, column);
                return Ok(res);
            }
            catch(ArgumentException) { return StatusCode(400);}
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/GameArea/Games/{id}
        [HttpDelete("Games/{id}")]
        public IActionResult DeleteGame(int id)
        {
            if (_gameService.DeleteGame(id))
                return Ok();
            return StatusCode(400);
        }
    }
}

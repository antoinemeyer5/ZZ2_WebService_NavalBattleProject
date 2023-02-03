using Microsoft.AspNetCore.Mvc;
using NavalWar.DTO;
using NavalWar.Business;
using NavalWar.DAL.Repositories;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bataille_Navale.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GameAreaController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameAreaController(IGameService gameService)
        {

            _gameService = gameService;

            PlayerDTO p = _gameService.CreatePlayer("coucou");
            PlayerDTO p1 = _gameService.CreatePlayer("c'est moi tchoupi");

            _gameService.HostGame(p);
            _gameService.JoinGame(p1);


            for (int i = 0; i < gameService.GetMap(0).Count; i++)
            {
                for (int j = 0; j < gameService.GetMap(0)[i].Count; j++)
                {
                    Console.Write(gameService.GetMap(0)[i][j] + "|");
                }
                Console.WriteLine();
            }
            for (int i = 0; i < gameService.GetMap(1).Count; i++)
            {
                for (int j = 0; j < gameService.GetMap(1)[i].Count; j++)
                {
                    Console.Write(gameService.GetMap(1)[i][j] + "|");
                }
                Console.WriteLine();
            }
        }

        [HttpGet("GetGameId")]
        // GET api/GameArea/GetGameId
        public int GetGameID()
        {

            return _gameService.GetId();
        }

        [HttpGet("Games/{id}")]
        //Get api/Games/{id}
        public IActionResult GetGame(int id)
        {
            GameDTO g =  _gameService.GetGame(id);
            if(g == null)
            {
                return StatusCode(400);
            }

            return Ok(g);
        }


        [HttpPut("Games")]
        // PUT api/Games
        public IActionResult NewGame()
        {
            GameDTO g = _gameService.CreateGame();
            
            if(g==null)
            {
                return StatusCode(400); 
            }

            return Ok(g);
        }


        [HttpGet("GetPlayerMap/{numPlayer}")]
        // GET api/GameArea/GetPlayerMap/{numPlayer}
        public List<List<int>> GetPlayerMap(int numPlayer)
        {
            return _gameService.ListMap[numPlayer].Body;
        }


        // PUT api/GameArea/numPlayer
        [HttpPut("PutShip/{numPlayer}")]
        public IActionResult PutShip(int numPlayer, [FromForm] int numShip, [FromForm] int line, [FromForm] int column, [FromForm] Orientation orientation)
        {
            try
            {
                _gameService.ListMap[numPlayer].PlaceShip(numShip, line, column, orientation);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(400);
            }

        }

        [HttpPut("Target/{numPlayer}")]
        // PUT api/GameArea/Target/numPlayer
        public IActionResult PutTarget(int numPlayer, [FromForm] int line, [FromForm] int column)
        {
            MapDTO target = numPlayer == 0 ? _gameService.ListMap[1] : _gameService.ListMap[0];
            try
            {
                string result = _gameService.ListMap[numPlayer].Target(line, column, target);
                return Ok(result);
            }
            catch
            (Exception)
            {
                return StatusCode(400);
            }
        }

        // DELETE api/GameArea/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteGame(int id)
        {
            if (_gameService.DeleteGame(id))
                return Ok();
            return StatusCode(400);
        }

        // PUT api/GameArea/CreatePlayer
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

        // Delete api/GameArea/Players/{playerId}
        [HttpDelete("Players/{playerId}")]
        public IActionResult DeletePlayer(int playerId)
        {
            if (_gameService.DeletePlayer(playerId))
                return Ok();

            return StatusCode(400); // We should not care ?
        }

        // Delete api/GameArea/Players/{playerId}
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

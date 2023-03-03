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
        public GameAreaController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("Games/{id}")]
        //Get api/Games/{id}
        public IActionResult GetGame(int id)
        {
            try
            {
                GameDTO g = _gameService.GetGame(id);
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
                GameDTO g = _gameService.GetGame(gameID);
                return Ok(g.ListMap[numPlayer]);    
            }
            catch(Exception)
            {
                return StatusCode(400);
            }
        }

        [HttpPut("Games")]
        // PUT api/GameArea/Games
        public IActionResult NewGame()
        {
            GameDTO g = _gameService.CreateGame();

            if (g == null)
            {
                return StatusCode(400);
            }
/*            for (int i = 0; i < g.ListMap[0].LineMax; i++)
            {
                for (int j = 0; j < g.ListMap[0].ColumMax; j++)
                {
                    Console.WriteLine("ee");
                    Console.WriteLine(g.ListMap[0].Body[i][j]);
                }
            }*/
            /*return Ok(JsonSerializer.Serialize(g));*/
            return Ok(g);

        }


        // PUT api/GameArea/Games/{gameID}/Putship/numPlayer
        [HttpPut("Games/{gameID}/PutShip/{numPlayer}")]
        public IActionResult PutShip(int gameID, int numPlayer, [FromForm] int numShip, [FromForm] int line, [FromForm] int column, [FromForm] Orientation orientation)
        {
            if(numPlayer< 0 || numPlayer > 1)
                return StatusCode(400);

            GameDTO g = _gameService.GetGame(gameID);
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
            bool res = _gameService.Target(gameID, numPlayer, line, column);
            if (res)
                return Ok("ciucou");

            return StatusCode(400);
        }

        // DELETE api/GameArea/Games/{id}
        [HttpDelete("Games/{id}")]
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


        // GET api/GameArea/Players
        [HttpGet("Players/{id}")]
        public IActionResult GetPlayer(int id) 
        {
            PlayerDTO p = _gameService.GetPlayer(id);
            if (p == null)
                return StatusCode(400);

            return Ok(p);
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

        // Manque associer player à MAP
        // Post api/GameArea/Games/{gameID}/associate/Players/{playerID}
        [HttpPost("Games/{gameID}/associate/Players/{playerID}")]
        public IActionResult AssociatePlayer(int gameID, int playerID, [FromForm] int id_secret_player )
        {
            if(playerID != 0 && playerID != 1)
                return StatusCode(400);

            if(!_gameService.AssociatePlayer(gameID, playerID, id_secret_player))
                return StatusCode(400);
            return Ok();
        }



    }
}

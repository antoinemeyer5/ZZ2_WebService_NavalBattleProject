using Microsoft.AspNetCore.Mvc;
using NavalWar.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bataille_Navale.Controllers
{ 

    [Route("api/[controller]")]
    [ApiController]
    public class GamingMap : ControllerBase
    {

        public static Game g = new Game();// public static List<Game> g = new List<Game>();

        public static int currentIdMax = 0;

        // TODO:
        // - Possibility to create a new game and save it into Database
        // - Autoincremente ID with database and not with the static int



        //It will have no sens when we will have different games
        [HttpGet("GetGameId")]
        // GET api/<GamingMap>/GetPlayerId
        public int GetGameID()
        {
            return g.idGame;
        }

        // A Player refers to a game, so we must take his map after identifying the player by his number and the id of the game
        // By REST norm we already know that we will take the map ("GamingMap" in URL) so we do not need to listen "GetPlayerMap"
        // Also they are required so I switched  for '{}' instead of '<>'
        // Finally, this is the API controler. Everything we do use is about request. So we do not need (and indeed we must avoid) to do 'normal' function.
        // We need to return status (OK/Error/...) and if we have to send things we just need to return it with the response (here it is send as json)
        [HttpGet("{GameId}/{PlayerId}")]
        // GET api/<GamingMap>/{GameId}/{PlayerId}
        public IActionResult GetPlayerMap(int GameId, int PlayerId) {
            if (PlayerId > 1 || PlayerId < 0)
            {
                return StatusCode(400);
            }

            // After having 'create fonction' we will use instead:
            /*
             * if(GameId not in the database) return StatusCode(400);
             * 
             * return Ok( g.Find(elt => elt.id == GameId).ListPlayer[PlayerId].Body);
            */
            return Ok( g.ListPlayer[PlayerId].Body);
        }


        // PUT api/<GamingMap>/numPlayer
        [HttpPut("PutShip/{numPlayer}")]
        public IActionResult PutShip(int numPlayer, [FromForm] int numShip, [FromForm] int line, [FromForm] int column, [FromForm] Orientation orientation)
        {
            try
            {
                g.ListPlayer[numPlayer].PlaceShip(numShip, line, column, orientation);
                return Ok();
            }
            catch(Exception)
            {
                return StatusCode(400);
            }
            
        }

        [HttpPut("Target/{numPlayer}")]
        public IActionResult PutTarget(int numPlayer, [FromForm] int line, [FromForm] int column)
        {
            Map target = numPlayer == 0 ? g.ListPlayer[1] : g.ListPlayer[0];
            try
            {
                string result = g.ListPlayer[numPlayer].Target(line, column, target);
                return Ok(result);
            }
            catch
            (Exception)
            {
                return StatusCode(400);
            }
        }

        // DELETE api/<GamingMap>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

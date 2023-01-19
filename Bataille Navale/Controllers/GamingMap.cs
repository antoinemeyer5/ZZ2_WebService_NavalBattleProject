using Microsoft.AspNetCore.Mvc;
using NavalWar.DTO;
using System.Diagnostics.Eventing.Reader;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bataille_Navale.Controllers
{ 

    [Route("api/[controller]")]
    [ApiController]
    public class GamingMap : ControllerBase
    {

        public static Game g = new Game();// public static List<Game> gameList = new List<Game>();

        public static int currentIdMax = 0;


        [HttpGet("Games")]
        // GET api/GamingMap/Games
        public IActionResult GetGames()
        {
            List<Game> list = FileStorage.LoadGame();
            
            // if null it's ok we send 'nothing'

            return Ok(list);
        }

        [HttpGet("Games/{id}")]
        // GET api/GamingMap/Games
        public IActionResult GetGame(int id)
        {
            List<Game> list = FileStorage.LoadGame();
            if(list == null)
            {
                return NotFound();
            }

            Game game = list.Find(elt => elt.idGame == id);

            if(game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        // A Player refers to a game, so we must take his map after identifying the player by his number and the id of the game
        // By REST norm we already know that we will take the map ("GamingMap" in URL) so we do not need to listen "GetPlayerMap"
        // Also they are required so I switched  for '{}' instead of '<>'
        // Finally, this is the API controler. Everything we do use is about request. So we do not need (and indeed we must avoid) to do 'normal' function.
        // We need to return status (OK/Error/...) and if we have to send things we just need to return it with the response (here it is send as json)
        [HttpGet("{gameId}/{playerId}")]
        // GET api/<GamingMap>/{gameId}/{playerId}
        public IActionResult GetPlayerMap(int gameId, int playerId) {
            if (playerId > 1 || playerId < 0)
            {
                return StatusCode(400);
            }

            // After having 'create fonction' we will use instead:
            /*
             * if(GameId not in the database) return StatusCode(400);
             * 
             * return Ok( gameList.Find(elt => elt.id == GameId).ListPlayer[PlayerId].Body);
            */
            return Ok( g.ListPlayer[playerId].Body);
        }


        // PUT api/<GamingMap>/playerId
        [HttpPut("/{gameId}/PutShip/{playerId}")]
        public IActionResult PutShip(int gameId, int playerId, [FromForm] int numShip, [FromForm] int line, [FromForm] int column, [FromForm] Orientation orientation)
        {
            try
            {
                g.ListPlayer[playerId].PlaceShip(numShip, line, column, orientation);
                return Ok();
            }
            catch(Exception)
            {
                return StatusCode(400);
            }
            /*
            Game game = gameList.Find(elt => elt.id == GameId);                 
            
            if(!game) return NotFound();

            try{
                game.ListPlayer[playerId].PlaceShip(numShip, line, column, orientation);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(400);
            }
            */

        }

        [HttpPut("/{gameId}/Target/{playerId}")]
        public IActionResult PutTarget(int gameId, int playerId, [FromForm] int line, [FromForm] int column)
        {
            Map target = (playerId == 0) ? g.ListPlayer[1] : g.ListPlayer[0];

            try
            {
                string result = g.ListPlayer[playerId].Target(line, column, target);
                return Ok(result);
            }
            catch
            (Exception)
            {
                return StatusCode(400);
            }

            /*Game game = gameList.Find(elt => elt.id == GameId);
            if (!game) return NotFound();
            try
            {
                string result = game.ListPlayer[playerId].Target(line, column, target);
                return Ok(result);
            }
            catch
            (Exception)
            {
                return StatusCode(400);
            }*/

        }

        // Necessity to create Data base and remove function
        // DELETE api/<GamingMap>/5
        [HttpDelete("{gameId}")]
        public IActionResult Delete(int gameId)
        {
            /*try
            {
                DataBase.remove(gameId);
            }
            catch (Exception)
            {
                // Does nothing if the game is not found => We do not need to earase something that doesn't exist
            }*/

            return Ok();
        }
    }
}

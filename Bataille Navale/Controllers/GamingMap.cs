using Microsoft.AspNetCore.Mvc;
using NavalWar.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bataille_Navale.Controllers
{ 

    [Route("api/[controller]")]
    [ApiController]
    public class GamingMap : ControllerBase
    {

        public static Game g = new Game();

        [HttpGet("GetGameId")]
        // GET api/<GamingMap>/GetPlayerId
        public int GetGameID()
        {
            return g.idGame;
        }

        [HttpGet("GetPlayerMap")]
        public List<List<int>> GetPlayerMap(int numPlayer) {
            return g.ListPlayer[numPlayer].Body;
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

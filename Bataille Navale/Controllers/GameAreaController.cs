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
        private readonly IPlayerRepository _playerRepository;

        public GameAreaController(IGameService gameService, IPlayerRepository playerRepository, IGameRepository gameRepository)
        {
            
            _playerRepository = playerRepository;
            _gameService = gameService;

            PlayerDTO p = _playerRepository.CreatePlayer("coucou");
            PlayerDTO p1 = _playerRepository.CreatePlayer("c'est moi tchoupi");
         
            _gameService.HostGame(p);
            _gameService.JoinGame(p1);  

            for(int i = 0; i < gameService.GetMap(0).Count; i++)
            {
                for(int j = 0; j < gameService.GetMap(0)[i].Count; j++)
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
        // GET api/<GamingMap>/GetPlayerId
        public int GetGameID()
        {

            return _gameService.getId();
        }

        [HttpGet("GetPlayerMap")]
        public List<List<int>> GetPlayerMap(int numPlayer) {
            return _gameService.ListMap[numPlayer].Body;
        }


        // PUT api/<GamingMap>/numPlayer
        [HttpPut("PutShip/{numPlayer}")]
        public IActionResult PutShip(int numPlayer, [FromForm] int numShip, [FromForm] int line, [FromForm] int column, [FromForm] Orientation orientation)
        {
            try
            {
                _gameService.ListMap[numPlayer].PlaceShip(numShip, line, column, orientation);
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

        // DELETE api/<GamingMap>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

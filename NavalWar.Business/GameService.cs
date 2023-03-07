using Microsoft.EntityFrameworkCore.Query;
using NavalWar.DAL.Models;
using NavalWar.DAL.Repositories;
using NavalWar.DTO;
using System.Numerics;

namespace NavalWar.Business
{
    // For void commit
    public class GameService : IGameService
    {
        private IGameRepository _gameRepository;
        public GameDTO GetGameByID(int id)
        {
            return _gameRepository.GetGame(id);
        }

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public GameDTO HostGame(PlayerDTO p)
        {
            GameDTO game = _gameRepository.CreateGame();
            _gameRepository.CreateMap(10, 10, game.IdGame, 0, p.Id);
            return GetGameByID(game.IdGame);
        }
        public void JoinGame(PlayerDTO p, int id)
        {
            GameDTO game = GetGameByID(id);
            _gameRepository.CreateMap(10, 10, game.IdGame, 1, p.Id);
        }

        public List<List<int>> GetMap(int idGame, int idPlayer)
        {
            return GetGameByID(idGame).ListMap[idPlayer].Body;
        }

        public MapDTO GetMapDTO(int gameID, int idPlayer)
        {
            GameDTO g = GetGameByID(gameID);
            MapDTO m = null;
            if (g != null)
            {
                m = g.ListMap[idPlayer];
            }

            return m;
        }
        public bool DeleteGame(int id)
        {
            return _gameRepository.DeleteGame(id);
        }
        public bool PutShip(int gameID, int numPlayer, int numShip, int line, int column, Orientation orientation)
        {
            return _gameRepository.PutShip(gameID, numPlayer, numShip, line, column, orientation);
        }

        public int Target(int gameID, int numPlayer, int line, int column)
        {
            return _gameRepository.Target(gameID, numPlayer, line, column);
        }

    }
}
using NavalWar.DTO;
using NavalWar.DAL.Repositories;
using NavalWar.DAL.Models;

namespace NavalWar.Business
{
    public class GameService : IGameService
    {
        private GameDTO _game;
        private IGameRepository _gameRepository;

        public MapDTO[] ListMap { get { return _game.ListMap; } set { _game.ListMap = value; } } 

        public GameService(int idGame, MapDTO m0, MapDTO m1)
        {
            _game = new GameDTO() { Result = -1, WinnerName = string.Empty, Duration = 0, IdGame = idGame };
            _game.ListMap[0] = m0;
            _game.ListMap[1] = m1;
        }

        public GameService(GameDTO g)
        {
            _game= g;
        }

        public int getId()
        {
            return _game.IdGame;
        }

        public GameService(IGameRepository gameRepository)
        {
            _game = null;
            _gameRepository = gameRepository;
        }

        public void HostGame(PlayerDTO p)
        {
            _game = _gameRepository.CreateGame();
            _game.ListMap[0] =  _gameRepository.CreateMap(10,10,_game.IdGame,p.Id);
        }
        public void JoinGame(PlayerDTO p)
        {
            _game.ListMap[1] = _gameRepository.CreateMap(10, 10, _game.IdGame, p.Id);
        }

        public List<List<int>> GetMap(int idPlayer)
        {
            return _game.ListMap[idPlayer].Body;
        }

    }

}
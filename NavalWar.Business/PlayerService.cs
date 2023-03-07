using NavalWar.DAL.Repositories;
using NavalWar.DTO;

namespace NavalWar.Business
{
    public class PlayerService : IPlayerService
    {
        private IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        public PlayerDTO UpdatePlayer(int playerId, string name)
        {
            return _playerRepository.UpdatePlayer(playerId, name);
        }

        public bool DeletePlayer(int playerId)
        {
            return _playerRepository.DeletePlayer(playerId);
        }


        public PlayerDTO CreatePlayer(string name)
        {
            return _playerRepository.CreatePlayer(name);
        }

        public PlayerDTO GetPlayer(int id)
        {
            return _playerRepository.GetPlayer(id);
        }
    }
}

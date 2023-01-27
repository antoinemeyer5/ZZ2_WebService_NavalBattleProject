using NavalWar.DTO;
using NavalWar.DAL.Repositories;
namespace NavalWar.Business
{
    public interface IGameService
    {
        public MapDTO[] ListMap { get; set; }
        public void HostGame(PlayerDTO p);

        public void JoinGame(PlayerDTO p);

        public int GetId();

        public List<List<int>> GetMap(int idPlayer);

        public bool DeleteGame(int id);

        public PlayerDTO UpdatePlayer(int playerId, string name);
        public bool DeletePlayer(int playerId);
        public PlayerDTO CreatePlayer(string name);

    }
}
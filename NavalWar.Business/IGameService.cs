using NavalWar.DTO;
namespace NavalWar.Business
{
    public interface IGameService
    {
        public GameDTO HostGame(PlayerDTO p);
        public void JoinGame(PlayerDTO p, int id);
        public GameDTO GetGameByID(int id);
        public List<List<int>> GetMap(int gameID, int idPlayer);
        public MapDTO GetMapDTO(int gameID, int idPlayer);
        public bool DeleteGame(int id);
        public bool PutShip(int gameID, int numPlayer,  int numShip, int line, int column, Orientation orientation);
        public int Target(int gameID, int numPlayer, int line, int column);

    }
}
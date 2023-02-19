using NavalWar.DTO;
namespace NavalWar.Business
{
    public interface IGameService
    {
        public MapDTO[] ListMap { get; set; }
        public void HostGame(PlayerDTO p);

        public void JoinGame(PlayerDTO p);

        public int GetId();

        public List<List<int>> GetMap(int idPlayer);

        public List<List<int>> GetMap(int gameID, int idPlayer);

        public MapDTO GetMapDTO(int gameID, int idPlayer);

        public bool DeleteGame(int id);

        public PlayerDTO UpdatePlayer(int playerId, string name);
        public bool DeletePlayer(int playerId);
        public PlayerDTO CreatePlayer(string name);

        public GameDTO GetGame(int id);

        public GameDTO CreateGame();

        public bool PutShip(int gameID, int numPlayer,  int numShip, int line, int column, Orientation orientation);
        public bool Target(int gameID, int numPlayer, int line, int column);

        public bool AssociatePlayer(int gameID, int playerID, int id_secret_player);

        public PlayerDTO GetPlayer(int id);
    }
}
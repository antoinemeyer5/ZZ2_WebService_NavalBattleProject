using NavalWar.DTO;

namespace NavalWar.Business
{
    public class Game : IGame
    {
        private int _result;
        private string _winnerName;
        private float _duration;

        public Map[] ListPlayer { get; } = new Map[2];
        public int IdGame { get; }

        public int getdata(int id) { return 1; }
        public Game(int idGame)
        {
            _result = -1;
            _winnerName = string.Empty;
            this.IdGame = idGame;
            ListPlayer[0] = new Map("Joueur1");
            ListPlayer[1] = new Map("Joueur2");
            _duration= 0;
        }

        public Game() : this(0)
        { }
    }

}
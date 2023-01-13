namespace NavalWar.DTO
{
    public class Game
    {
        private int _result;
        private string _winnerName;
        private float _duration;

        public Map[] ListPlayer { get; } = new Map[2];
        public int idGame { get; }

        public Game(int idGame)
        {
            _result = -1;
            _winnerName = string.Empty;
            this.idGame = idGame;
            ListPlayer[0] = new Map("Joueur1");
            ListPlayer[1] = new Map("Joueur2");
            _duration= 0;
        }

        public Game() : this(0)
        { }
    }
}
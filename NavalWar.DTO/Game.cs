using System.Reflection.Metadata.Ecma335;

namespace NavalWar.DTO
{
    [Serializable]
    public class Game
    {
        private int _result;
        private string _winnerName;
        private float _duration;

        public Map[] ListPlayer { get; } = new Map[2];
        public int idGame { get; set; } //Here we need the set or even with constructor it will not change

        public Game(int idGame)
        {
            _result = -1;
            _winnerName = string.Empty;
            this.idGame = FileStorage.LoadGame().Max(elt => elt.idGame)+1;
            ListPlayer[0] = new Map("Joueur1");
            ListPlayer[1] = new Map("Joueur2");
            _duration= 0;
        }

        public Game() : this(0)
        { }
    }
}
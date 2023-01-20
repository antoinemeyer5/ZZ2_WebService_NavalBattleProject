using NavalWar.DTO;

namespace NavalWar.Business
{
    public class Game : IGame
    {
        private int _result;
        private string _winnerName;
        private float _duration;

        public Map[] ListMap { get; } = new Map[2];
        public int IdGame { get; }
        public int getdata(int id) { return 1; }
        public Game(int idGame)
        {
            _result = -1;
            _winnerName = string.Empty;
            this.IdGame = idGame;
            ListMap[0] = new Map("Joueur1");
            ListMap[1] = new Map("Joueur2");
            _duration= 0;
        }

        public Game() : this(0)
        { }

        public void SetPlayer(Player player)
        {
            if(compteur == 2)
            {
                Console.WriteLine("Maximum number of players reached\n");
            }
            else
            {
                ListMap[compteur].setPlayer(player); 
                compteur++;
            }

        }
    }

}
using Microsoft.VisualBasic;



namespace NavalWar.DTO
{
    public enum Orientation { HORIZONTAL, VERTICAL };


    public class Ship
    {
        public int Pv { get; set; }

        public string Name { get; set; }
        public (int, int) position { get; set; }

        public int Length { get; set; }

        public Orientation Orientation { get; set; }

        public Ship(string name, (int, int) position, int length, Orientation orientation)
        {
            Name = name;
            this.position = position;
            Length = Pv = length;
            Orientation = orientation;
        }
    }

    public class Carrier : Ship
    {
        public Carrier((int, int) position, Orientation orientation) : base("Carrier", position, 5, orientation)
        { }
    }

    public class Battleship : Ship
    {
        public Battleship((int, int) position, Orientation orientation) : base("BattleShip", position, 4, orientation)
        { }
    }

    public class Destroyer : Ship
    {
        public Destroyer((int, int) position, Orientation orientation) : base("Destroyer", position, 3, orientation)
        { }
    }

    public class Submarine : Ship
    {
        public Submarine((int, int) position, Orientation orientation) : base("Submarine", position, 3, orientation)
        { }
    }
    public class PatrolBoat : Ship
    {
        public PatrolBoat((int, int) position, Orientation orientation) : base("PatrolBoat", position, 2, orientation)
        { }
    }
    public class Player
    {

        private static Random _rand = new Random();
        public int Id { get; }

        private List<Ship> _shipList;

        private int _lineMax = 12;
        private int _columnMax = 12;

        public string Name { get; set; }

        public List<List<int>> Body { get; }
        public int ColumMax { get { return _columnMax; } }
        public int LineMax { get { return _lineMax; } }

        private HashSet<(int, int)> _listTarget = new HashSet<(int, int)>();

        public Player(string name, int lineMax, int columnMax)
        {

            Id = _rand.Next();
            Console.WriteLine($"Player: {Id}");
            Name = name;

            _shipList = new List<Ship>
            {
                 new Carrier((-1,-1),Orientation.HORIZONTAL),
                 new Battleship((-1,-1),Orientation.HORIZONTAL),
                 new Destroyer((-1 , -1),Orientation.HORIZONTAL),
                 new Submarine((-1 , -1),Orientation.HORIZONTAL),
                 new PatrolBoat((-1 , -1),Orientation.HORIZONTAL)
            };

            _lineMax = lineMax;
            _columnMax = columnMax;

            Body = new List<List<int>>();
            for (int i = 0; i < lineMax; i++)
            {
                Body.Add(Enumerable.Repeat(-1, columnMax).ToList());
            }

        }
        public Player(string name) : this(name, 12, 12) { }

        public void PlaceShip(int numShip, int line, int column, Orientation orientation)
        {
            Ship value = _shipList[numShip];
            var (dep_x, dep_y) = orientation == Orientation.HORIZONTAL ? (0, 1) : (1, 0);


            if (line + dep_x * value.Length >= LineMax || column + dep_y * value.Length >= ColumMax)
                throw new ArgumentException();
            for (int i = 0; i < value.Length; i++)
            {
                if (Body[line + dep_x * i][column + dep_y * i] != -1 && Body[line + dep_x * i][column + dep_y * i] != numShip)
                {
                    throw new ArgumentException();
                }
            }

            (dep_x, dep_y) = value.Orientation == Orientation.HORIZONTAL ? (0, 1) : (1, 0);
            if (value.position != (-1, -1))
            {
                for (int i = 0; i < value.Length; i++)
                {
                    Body[value.position.Item1 + dep_x * i][value.position.Item2 + dep_y * i] = -1;
                }
            }

            value.position = (line, column);
            value.Orientation = orientation;
            (dep_x, dep_y) = value.Orientation == Orientation.HORIZONTAL ? (0, 1) : (1, 0);

            for (int i = 0; i < value.Length; i++)
            {
                Body[value.position.Item1 + dep_x * i][value.position.Item2 + dep_y * i] = numShip;
            }
        }

        public string Target(int line, int column, Player target)
        {
            if (_listTarget.Contains((line, column)) || line >= LineMax || column >= ColumMax)
            {
                throw new ArgumentException();
            }
            _listTarget.Add((line, column));
            return target.ReceiveTarget(line, column);
        }

        public string ReceiveTarget(int line, int column)
        {
            string result;
            if (Body[line][column] >= 0)
            {
                Ship ship = _shipList[Body[line][column]];
               
                ship.Pv--;
                if (ship.Pv == 0)
                    result = "Coulé!";
                else
                    result = "Touché !";
                
            }
            else
                result = "A l'eau";

            Body[line][column] = -2;
            return result;
        }
    }
    public class GameManager
    {
        private Player _player1;
        private Player _player2;

        public Player[] ListPlayer { get; } = new Player[2];
        public int[] PlayerID { get; } = new int[2];
        public int idGame { get; }

        public GameManager(int idGame)
        {
            _player1 = new Player("Joueur1");
            _player2 = new Player("Joueur2");
            PlayerID[0] = _player1.Id;
            PlayerID[1] = _player2.Id;

            ListPlayer[0] = _player1;
            ListPlayer[1] = _player2;
            Console.WriteLine("Game create");

        }

        public GameManager() : this(0)
        { }
    }
}
using Microsoft.VisualBasic;



namespace NavalWar.DTO
{
    public enum Orientation { HORIZONTAL, VERTICAL };
    public class MapDTO
    {
        private List<Ship> _associatedShips;  //example: [ship1; ship3; ship4]
        private int _lineMax = 10;
        private int _columnMax = 10;

        public List<List<int>> Body { get; set; }
        public int ColumMax { get { return _columnMax; } }
        public int LineMax { get { return _lineMax; } }

        private HashSet<(int, int)> _listTarget = new HashSet<(int, int)>();

        public PlayerDTO AssociatedPlayer { get; set; }

        public MapDTO(int lineMax, int columnMax)
        {

            _associatedShips = new List<Ship>
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
        public MapDTO() : this(10, 10) { }

        public void PlaceShip(int numShip, int line, int column, Orientation orientation)
        {
            Ship value = _associatedShips[numShip];
            var (dep_x, dep_y) = (orientation == Orientation.HORIZONTAL) ? (0, 1) : (1, 0);


            if (line + dep_x * value.Length >= LineMax || column + dep_y * value.Length >= ColumMax)
                throw new ArgumentException();
            for (int i = 0; i < value.Length; i++)
            {
                if (Body[line + dep_x * i][column + dep_y * i] != -1 && Body[line + dep_x * i][column + dep_y * i] != numShip)
                {
                    throw new ArgumentException();
                }
            }

            (dep_x, dep_y) = (value.Orientation == Orientation.HORIZONTAL) ? (0, 1) : (1, 0);
            if (value.position != (-1, -1))
            {
                for (int i = 0; i < value.Length; i++)
                {
                    Body[value.position.Item1 + dep_x * i][value.position.Item2 + dep_y * i] = -1;
                }
            }

            value.position = (line, column);
            value.Orientation = orientation;
            (dep_x, dep_y) = (value.Orientation == Orientation.HORIZONTAL) ? (0, 1) : (1, 0);

            for (int i = 0; i < value.Length; i++)
            {
                Body[value.position.Item1 + dep_x * i][value.position.Item2 + dep_y * i] = numShip;
            }
        }

        public string Target(int line, int column, MapDTO target)
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
                Ship ship = _associatedShips[Body[line][column]];

                ship.Pv--;
                if (ship.Pv == 0)
                    result = "Coulé!";
                else
                    result = "Touché !";

                Body[line][column] = -2;
            }
            else
            {
                result = "A l'eau";
                Body[line][column] = -3;
            }
            return result;
        }
    }
}
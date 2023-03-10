using System.Numerics;

namespace NavalWar.DTO
{
    public enum Orientation { HORIZONTAL, VERTICAL };
    public class MapDTO
    {
       
        private int _lineMax = 10;
        private int _columnMax = 10;

        public List<List<int>> Body { get; set; }
        public int ColumMax { get { return _columnMax; } }
        public int LineMax { get { return _lineMax; } }
        public static List<Ship> AssociatedShips { get; set; } = new List<Ship>(){ 
                 new Carrier((-1,-1),Orientation.HORIZONTAL),
                 new Battleship((-1,-1),Orientation.HORIZONTAL),
                 new Destroyer((-1 , -1),Orientation.HORIZONTAL),
                 new Submarine((-1 , -1),Orientation.HORIZONTAL),
                 new PatrolBoat((-1 , -1),Orientation.HORIZONTAL) };

        public HashSet<Vector2> ListTarget { get; set; }

        public PlayerDTO? AssociatedPlayer { get; set; }

        public MapDTO(int lineMax, int columnMax)
        {
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
            Ship value = AssociatedShips[numShip];
            var (dep_x, dep_y) = (orientation == Orientation.HORIZONTAL) ? (0, 1) : (1, 0);

            // Check index
            if ( (line + dep_x * (value.Length-1)) >= LineMax || (column + dep_y * (value.Length-1)) >= ColumMax)
                throw new ArgumentException();

            // Check free case 
            for (int i = 0; i < value.Length; i++)
            {
                if (Body[line + dep_x * i][column + dep_y * i] != -1 && Body[line + dep_x * i][column + dep_y * i] != numShip)
                {
                    throw new ArgumentException();
                }
            }


            (dep_x, dep_y) = (value.Orientation == Orientation.HORIZONTAL) ? (0, 1) : (1, 0);
            // Already placed ?
            if (value.position != (-1, -1))
            {
                // We erase the ship from the map
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

        public int Target(int line, int column, MapDTO target)
        {
            if (ListTarget.Contains(new Vector2(line, column)) || line >= LineMax || column >= ColumMax)
            {
                Console.WriteLine("already targeted or out of map");
                throw new ArgumentException();
            }
            ListTarget.Add(new Vector2{ X=line,Y=column});
            return target.ReceiveTarget(line, column);
        }

        public int ReceiveTarget(int line, int column)
        {
            int result;
            if (Body[line][column] >= 0)
            {
                Ship ship = AssociatedShips[Body[line][column]];

                //ship.Pv--;
                //if (ship.Pv == 0)
                    result = 2;
                //else
                //    result = 1;

                Body[line][column] = -2;
            }
            else
            {
                result = 0;
                Body[line][column] = -3;
            }
            return result;
        }
    }
}
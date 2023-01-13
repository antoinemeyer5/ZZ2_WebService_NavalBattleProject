namespace NavalWar.DTO
{
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
}
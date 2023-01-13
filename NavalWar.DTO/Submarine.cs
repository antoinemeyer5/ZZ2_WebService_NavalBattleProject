namespace NavalWar.DTO
{
    public class Submarine : Ship
    {
        public Submarine((int, int) position, Orientation orientation) : base("Submarine", position, 3, orientation)
        { }
    }
}
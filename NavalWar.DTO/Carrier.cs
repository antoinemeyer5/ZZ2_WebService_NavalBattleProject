namespace NavalWar.DTO
{
    public class Carrier : Ship
    {
        public Carrier((int, int) position, Orientation orientation) : base("Carrier", position, 5, orientation)
        { }
    }
}
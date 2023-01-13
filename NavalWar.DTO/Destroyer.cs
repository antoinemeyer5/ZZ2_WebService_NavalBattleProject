namespace NavalWar.DTO
{
    public class Destroyer : Ship
    {
        public Destroyer((int, int) position, Orientation orientation) : base("Destroyer", position, 3, orientation)
        { }
    }
}
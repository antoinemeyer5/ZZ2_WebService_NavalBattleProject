namespace NavalWar.DTO
{
    public class PatrolBoat : Ship
    {
        public PatrolBoat((int, int) position, Orientation orientation) : base("PatrolBoat", position, 2, orientation)
        { }
    }
}
namespace NavalWar.DTO
{
    public class Battleship : Ship
    {
        public Battleship((int, int) position, Orientation orientation) : base("BattleShip", position, 4, orientation)
        { }
    }
}
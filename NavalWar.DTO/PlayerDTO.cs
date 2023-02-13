namespace NavalWar.DTO
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> History { get; set; } // Id of previous games plays
    }
}

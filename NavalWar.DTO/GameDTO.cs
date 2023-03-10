namespace NavalWar.DTO
{
    public class GameDTO
    {
        public bool TourA { get; set; }
        public int IdGame { get; set; }

        public int Result { get; set; }
        public int WinnerId { get; set; }
        public float Duration { get; set; }
        public MapDTO[] ListMap { get; set; } = new MapDTO[2];

    }
}

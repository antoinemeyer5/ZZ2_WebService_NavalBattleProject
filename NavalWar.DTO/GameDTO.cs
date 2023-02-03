namespace NavalWar.DTO
{
    public class GameDTO
    {
        private static int compteur = 0; // Ask the TeaSheur: Should we do it DB side ? (I guess yes but I won't moove everything rn without your permissions)
        public int IdGame { get; set; }

        public int Result { get; set; }
        public string WinnerName { get; set; }
        public float Duration { get; set; }
        public MapDTO[] ListMap { get; set; } = new MapDTO[2];

        public GameDTO()
        {
            IdGame = compteur;
            compteur++;
        }
    }
}

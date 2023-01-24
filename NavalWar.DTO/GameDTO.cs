using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.DTO
{
    public class GameDTO
    {
        public int IdGame { get; set; }

        public int Result { get; set; }
        public string WinnerName { get; set; }
        public float Duration { get; set; }
        public MapDTO[] ListMap { get; set; } = new MapDTO[2];
    }
}

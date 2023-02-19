using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NavalWar.DTO;
using System.ComponentModel.DataAnnotations;

namespace NavalWar.DAL.Models
{
    public class Game
    {
        [Key]
        public int IdGame { get; set; }
        public Map Map0{ get; set; }
        public Map Map1 { get; set; }
        public int idMap0 { get; set; }
        public int idMap1 { get; set; }
        public int Result { get; set; }
        public int WinnerId { get; set; }
        public float Duration { get; set; }

    }
}
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NavalWar.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace NavalWar.DAL.Models
{
    public class Game
    {
        [Key]
        public int IdGame { get; set; }
        public int? idMap0 { get; set; }
        public int? idMap1 { get; set; }
        public int Result { get; set; }
        public int WinnerId { get; set; }
        public float Duration { get; set; }

        public virtual Map Map0 { get; set; }
        public virtual Map Map1 { get; set; }

    }
}
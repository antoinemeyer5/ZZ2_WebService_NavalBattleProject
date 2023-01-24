using NavalWar.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.DAL.Models
{
    public class Map
    {
        [Key]
        public int IdMap { get; set; }
        public int IdInGame { get; set; }
        public int Column { get; set; }
        public int Line { get; set; }

        public string Body { get; set; }

        [StringLength(1000)]
        public string ListTarget {get;set;}

        public Player _associatedPlayer { get; set; }
    }
}

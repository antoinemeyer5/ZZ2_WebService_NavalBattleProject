using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        public int Column { get; set; } = 10;
        public int Line { get; set; } = 10;

        public string Body { get; set; } = string.Empty;

        [StringLength(1000)]
        public string ListTarget {get;set;} = string.Empty;

        public Player? _associatedPlayer { get; set; } = null;
        public int? idPlayer { get; set; } = null;

    }

    

}

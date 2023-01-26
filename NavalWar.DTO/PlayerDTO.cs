using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.DTO
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public string Name {get; set;}
        public List<int> History { get; set; } // Id of previous games plays
    }
}

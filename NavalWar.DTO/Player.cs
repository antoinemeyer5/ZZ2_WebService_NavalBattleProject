using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.DTO
{
    public class Player
    {
        public int Id { get; }
        string name;
        List<int> history; // Id of previous games plays
    }
}

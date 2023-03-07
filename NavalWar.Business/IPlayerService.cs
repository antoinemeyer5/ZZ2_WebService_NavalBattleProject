using NavalWar.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.Business
{
    public interface IPlayerService
    {
        public PlayerDTO CreatePlayer(string name);
        public bool DeletePlayer(int playerId);
        public PlayerDTO GetPlayer(int id);
        public PlayerDTO UpdatePlayer(int playerId, string name);
    }
}

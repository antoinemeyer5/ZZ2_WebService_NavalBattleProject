using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using ExtensionMethod;
using NavalWar.DAL.Models;
using NavalWar.DTO;

namespace NavalWar.DAL.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly NavalContext _context;

        public GameRepository(NavalContext context)
        {
            _context = context;
        }

        public MapDTO CreateMap(int line, int column, int idInGame, int idPlayer)
        {
            Map m = new Map() { Line = line, Column = column, IdInGame = idInGame, _associatedPlayer = _context.Players.First(x => x.Id == idPlayer) };

            /*string body = "";
            for (int i = 0; i < line * column; i++)
            {
                body += "-1|";
            }
            m.ListTarget = "";
            m.Body = body;*/

            m.Body = new List<List<int>>();
            for (int i = 0; i < line; i++)
            {
                List<int> list = new List<int>();
                for (int j = 0; j < column; j++)
                    list.Add(-1);
                m.Body.Add(list);
            }

            _context.Maps.Add(m);
            /*_context.SaveChanges();*/
            _context.SaveChangesAsync(); // Asynchronously because we will manage so many players that it could make some problems? Ask the T-shirt (Tea Sheur)


            return m.toDTO();
        }

        public GameDTO CreateGame()
        {
            Game g = new Game() { Result = -1, WinnerName = string.Empty, Duration = 0 };
            return g.toDTO();
        }

        public bool DeleteGame(int id)
        {
            Game g = _context.Games.Find(id);

            if(g == null)
                return false;

            // We must destroy associated maps right ?
            if (g.Map0 != null)
            {
                _context.DeleteMaps(g.Map0.IdMap);
            }

            if (g.Map1 != null)
            {
                _context.DeleteMaps(g.Map1.IdMap);
            }
            _context.Games.Remove(g);
            _context.SaveChanges();

            return true;
        
        }



    }
}

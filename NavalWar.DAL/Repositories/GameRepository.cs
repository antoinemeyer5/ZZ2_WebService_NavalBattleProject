using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using ExtensionMethod;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

            m.Body = new List<List<int>>();
            for (int i = 0; i < line; i++)
            {
                List<int> list = new List<int>();
                for (int j = 0; j < column; j++)
                    list.Add(-1);
                m.Body.Add(list);
            }
            m.ListTarget = string.Empty;
            _context.Maps.Add(m);

            // -- Point to think about: Id generated when we save the game but becarefull, game saved only if we did another action
            //_context.SaveChanges();


            return m.toDTO();
        }

        public GameDTO CreateGame()
        {
            Game g = new Game() { Result = -1, WinnerName = string.Empty, Duration = 0 };

            // -- Point to think about: Id generated when we save the game but becarefull, game saved only if we did another action

            int id = _context.Games.Max(elt => elt.IdGame);
            g.IdGame= id;   

            _context.Games.Add(g);


            return g.toDTO();
        }

        public GameDTO GetGame(int id)
        {
            Game g = _context.Games.Find(id);

            if (g == null)
                return null;

            return g.toDTO();
        }

        public bool DeleteGame(int id)
        {
            Game g = _context.Games.Find(id);

            if(g == null)
                return false;

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

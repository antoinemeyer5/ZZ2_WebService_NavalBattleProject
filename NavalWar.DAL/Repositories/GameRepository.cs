using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            List<List<int>> tempBody = new List<List<int>>();

            for (int i = 0; i < line; i++)
            {
                List<int> list = new List<int>();
                for (int j = 0; j < column; j++)
                    list.Add(-1);
                tempBody.Add(list);
            }
            m.Body = JsonSerializer.Serialize(tempBody, (JsonSerializerOptions)null);

            m.ListTarget = string.Empty;
            _context.Maps.Add(m);
            _context.SaveChanges();


            return m.toDTO();
        }

        public GameDTO CreateGame()
        {
            Game g = new Game() { Result = -1, WinnerId = -1, Duration = 0 };

            // -- Point to think about: Id generated when we save the game but becarefull, game saved only if we did another action
            int id;
            if (_context.Games.Count() == 0)
            {
                id = 0;
            }
            else
            {
                id = _context.Games.Max(elt => elt.IdGame);
            }
            g.IdGame= id;



            g.Map0 = new Map();
            g.Map1 = new Map();

            _context.Maps.Add(g.Map0);
            _context.Maps.Add(g.Map1);
            _context.Games.Add(g);
            _context.SaveChanges();


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

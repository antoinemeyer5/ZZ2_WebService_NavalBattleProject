using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;
using ExtensionMethod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NavalWar.DAL.Models;
using NavalWar.DTO;
using System.Numerics;

namespace NavalWar.DAL.Repositories
{
    
    public class GameRepository : IGameRepository
    {
        private readonly NavalContext _context;

        public GameRepository(NavalContext context)
        {
            _context = context;
        }

        public MapDTO CreateMap(int line, int column, int idGame, int idInGame, int idPlayer)
        {
            Game g = _context.Games.First(game => game.IdGame == idGame);
            Player p = _context.Players.First(p => p.Id == idPlayer);
            Map m = new Map() { Line = line, Column = column, IdInGame = idInGame, _associatedPlayer = p, idPlayer = p.Id};
            List<List<int>> tempBody = new List<List<int>>();

            for (int i = 0; i < line; i++)
            {
                List<int> list = new List<int>();
                for (int j = 0; j < column; j++)
                    list.Add(-1);
                tempBody.Add(list);
            }

            HashSet<(int, int)> h = new HashSet<(int, int)>();
            m.Body = JsonSerializer.Serialize(tempBody, (JsonSerializerOptions)null);
            m.ListTarget = JsonSerializer.Serialize(h, (JsonSerializerOptions)null);

            if (idInGame == 0)
            {
                g.Map0 = m;
            }
            else
            {
                g.Map1 = m;
            }

            _context.Maps.Add(m);
            _context.SaveChanges();

            return m.toDTO();
        }

    public GameDTO CreateGame()
        {
            Game g = new Game() { Result = -1, WinnerId = -1, Duration = 0 };
            _context.Games.Add(g);
            _context.SaveChanges();
            return g.toDTO();
        }

        
        public IQueryable<Game> IncludeAll(IQueryable<Game> query)
        {
            return query.Include(x => x.Map0)
                        .Include(x => x.Map1)
                        .Include(x => x.Map0._associatedPlayer)
                        .Include(x => x.Map1._associatedPlayer);
        }

        public GameDTO GetGame(int id)
        {
            Game g = IncludeAll(_context.Games).First(game => game.IdGame == id);
            return g.toDTO();
        }

        public bool DeleteGame(int id)
        {
            Game g = _context.Games.Find(id);

            if (g == null)
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


        public bool PutShip(int gameID, int numPlayer, int numShip, int line, int column, Orientation orientation)
        {
            Game g = _context.Games.Find(gameID);
            if (g == null)
                return false;
            Map m = null;
            if (numPlayer == 0)
                m = _context.Maps.Find(g.idMap0);
            else
                m = _context.Maps.Find(g.idMap1);

            if (m == null)
                return false;

            MapDTO mapDTO = m.toDTO();

            try
            {
                Ship value = mapDTO.AssociatedShips[numShip];
                var (dep_x, dep_y) = (orientation == Orientation.HORIZONTAL) ? (0, 1) : (1, 0);

                // Check index
                if ((line + dep_x * (value.Length - 1)) >= mapDTO.LineMax || (column + dep_y * (value.Length - 1)) >= mapDTO.ColumMax)
                    throw new ArgumentException();

                // Check free case 
                for (int i = 0; i < value.Length; i++)
                {
                    if (mapDTO.Body[line + dep_x * i][column + dep_y * i] != -1 && mapDTO.Body[line + dep_x * i][column + dep_y * i] != numShip)
                    {
                        throw new ArgumentException();
                    }
                }

                (dep_x, dep_y) = (value.Orientation == Orientation.HORIZONTAL) ? (0, 1) : (1, 0);
                // Already placed ?
                if (value.position != (-1, -1))
                {
                    // We erase the ship from the map
                    for (int i = 0; i < value.Length; i++)
                    {
                        mapDTO.Body[value.position.Item1 + dep_x * i][value.position.Item2 + dep_y * i] = -1;
                    }
                }

                value.position = (line, column);
                value.Orientation = orientation;
                (dep_x, dep_y) = (value.Orientation == Orientation.HORIZONTAL) ? (0, 1) : (1, 0);

                for (int i = 0; i < value.Length; i++)
                {
                    mapDTO.Body[value.position.Item1 + dep_x * i][value.position.Item2 + dep_y * i] = numShip;
                }

                m.Body = JsonSerializer.Serialize(mapDTO.Body);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public int Target(int gameID, int numPlayer, int line, int column)
        {
            Game g = IncludeAll(_context.Games).First(x=> x.IdGame == gameID);
            if (g == null)
                return 0;

            Map attacker = numPlayer == 0 ? _context.Maps.Include(x => x._associatedPlayer).First(x => x.IdMap == g.idMap0): _context.Maps.Include(x => x._associatedPlayer).First(x => x.IdMap == g.idMap1);
            Map target = numPlayer == 0 ? _context.Maps.Include(x => x._associatedPlayer).First(x => x.IdMap == g.idMap1) : _context.Maps.Include(x => x._associatedPlayer).First(x => x.IdMap == g.idMap0);
          
            if (attacker == null || target == null)
                throw new ArgumentException();

            MapDTO attackerDTO = attacker.toDTO();
            MapDTO targetDTO = target.toDTO();

            if (attackerDTO.ListTarget.Contains(new Vector2(line, column)) || line >= attackerDTO.LineMax || column >= attackerDTO.ColumMax)
            {
                Console.WriteLine("already targeted or out of map");
                throw new ArgumentException();
            }

            int result;
            try
            {
                attackerDTO.ListTarget.Add(new Vector2 { X = line, Y = column });
          
                
                if (targetDTO.Body[line][column] >= 0)
                {
                    Ship ship = targetDTO.AssociatedShips[targetDTO.Body[line][column]];

                    //ship.Pv--;
                    //if (ship.Pv == 0)
                    //result = 2;
                    //else
                        result = 1;

                    targetDTO.Body[line][column] = -2;
                }
                else
                {
                    result = 0;
                    targetDTO.Body[line][column] = -3;
                }
                target.Body = JsonSerializer.Serialize(targetDTO.Body);


                var options = new JsonSerializerOptions();
                options.Converters.Add(new VectorConverter());
                attacker.ListTarget = JsonSerializer.Serialize(attackerDTO.ListTarget, options);
                _context.SaveChanges();

                Console.WriteLine(attacker.ListTarget);
            }
            catch (Exception)
            {
                return 0;
            }

            return result;
        }
    }
}

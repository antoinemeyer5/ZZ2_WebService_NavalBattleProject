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

            Map m1 = new Map();
            Map m2 = new Map();

            _context.Maps.Add(m1);
            _context.Maps.Add(m2);
            _context.SaveChanges();

            g.Map0= m1;
            g.Map1= m2;
            _context.Games.Add(g);
            _context.SaveChanges();

            return g.toDTO();
        }

        public GameDTO GetGame(int id)
        {
            Game g = _context.Games.FirstOrDefault(game => game.IdGame == id);
            
            if (g == null)
                return null;

            Map m0 = _context.Maps.FirstOrDefault(m => m.IdMap == g.idMap0);
            Map m1 = _context.Maps.FirstOrDefault(m => m.IdMap == g.idMap1);

            Player p0 = _context.Players.FirstOrDefault(p => m0.idPlayer == id);
            Player p1 = _context.Players.FirstOrDefault(p => m1.idPlayer == id);

            m0._associatedPlayer = p0;
            m1._associatedPlayer = p1;

            g.Map0 = m0;
            g.Map1 = m1;
            
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

            MapDTO m1 = m.toDTO();

            try
            {
                m1.PlaceShip(numShip, line, column, orientation);
                m.Body = JsonSerializer.Serialize(m1.Body);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Target(int gameID, int numPlayer, int line, int column)
        {
            Game g = _context.Games.Find(gameID);
            if(g== null)
                return false;

            Map m = null;
            if (numPlayer == 0)
                m = _context.Maps.Find(g.idMap1);
            else
                m = _context.Maps.Find(g.idMap0);

            if (m == null)
                return false;
            
            MapDTO m1 = m.toDTO();

            // DEBUG: penser à modifier plus tard pour retour en fonction res (savoir le pouruqoi)
            try
            {
                string s = m1.Target(line, column, m1);
                if(s == "Touché !" || s == "Coulé!")
                {
                    m.Body = JsonSerializer.Serialize(m1.Body);
                    _context.SaveChanges();
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        public bool AssociatePlayer(int gameID, int playerID, int id_secret_player)
        {
            // Security : check if already has one ?
            Game g = _context.Games.FirstOrDefault(game => game.IdGame == gameID);

            if (g == null) return false;

            Player p = _context.Players.FirstOrDefault(p => p.Id == id_secret_player);

            Map m;
            if (playerID == 0)
            {
                m = _context.Maps.FirstOrDefault(m => m.IdMap == g.idMap0);
            }
            else
            {
                m = _context.Maps.FirstOrDefault(m => m.IdMap == g.idMap1);
            }

            if(p ==null || m == null) return false;

            m._associatedPlayer= p;
            m.idPlayer = p.Id;
            _context.SaveChanges();

            if (playerID == 0)
            {
               g.Map0 = m;
            }
            else
            {
                g.Map1 = m;
            }
            _context.SaveChanges();



            return true;
        }

    }
}

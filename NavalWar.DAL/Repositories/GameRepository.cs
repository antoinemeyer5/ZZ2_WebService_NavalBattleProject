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

        public Map CreateMap(int line, int column)
        {
            Map m = new Map() { Line = line, Column = column};
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

            return m;
        }

        public GameDTO CreateGame()
        {
            Game g = new Game() { Result = -1, WinnerId = -1, Duration = 0 };
            

            Map m1 = CreateMap(10, 10);
            Map m2 = CreateMap(10, 10);

            g.Map0= m1;
            g.Map1= m2;
            g.idMap0 = m1.IdMap;
            g.idMap1 = m2.IdMap;

            _context.Games.Add(g);
            _context.SaveChanges();

            Console.WriteLine("idmap: " + g.idMap0 +  " " + g.idMap1);


            return g.toDTO();
        }

        public GameDTO GetGame(int id)
        {
            Game g = _context.Games.FirstOrDefault(game => game.IdGame == id);
            
            if (g == null)
                return null;

            Map m0 = _context.Maps.FirstOrDefault(m => m.IdMap == g.idMap0);
            Map m1 = _context.Maps.FirstOrDefault(m => m.IdMap == g.idMap1);

            if (m0.idPlayer != null)
            {
                Player p0 = _context.Players.Find(m0.idPlayer);
                m0._associatedPlayer = p0;
            }

            if (m1.idPlayer != null)
            {
                Player p1 = _context.Players.Find(m1.idPlayer);
                m1._associatedPlayer = p1;
            }

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

            if (playerID == 0)
            {
               g.Map0 = m;
                Console.WriteLine("Je mets dans la map 0 :" + m.idPlayer + " - " + p.Id + " - " + g.Map0.idPlayer);
            }
            else
            {
                g.Map1 = m;
                Console.WriteLine("Je mets dans la map 1 :" + m.idPlayer + " - " + p.Id + " - " + g.Map1.idPlayer);
            }
            _context.SaveChanges();
            Console.WriteLine("J'enregistre");

            

            return true;
        }

    }
}

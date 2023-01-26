using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavalWar.DTO;
using ExtensionMethod;
using NavalWar.DAL.Models;

namespace NavalWar.DAL.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly NavalContext _context;

        public PlayerRepository(NavalContext context)
        {
            _context = context;
        }

        public PlayerDTO GetPlayer(int id)
        {
            try
            {
                Player player = _context.Players.First(x => x.Id == id);
                return player.toDTO();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PlayerDTO CreatePlayer(string name) {
            var p = new Player() { Name = name} ;
            _context.Players.Add(p);
            _context.SaveChanges();
            return p.toDTO();
        }

        public bool DeletePlayer(int id)
        {
            Player p = _context.Players.Find(id);
            if(p != null)
            { 
                _context.Players.Remove(p);
                _context.SaveChanges();
                return true;
            }

            return false;   
        }
           
        // Only the name can be changed
        public bool UpdatePlayer(int id, string name)
        {
            Player p = _context.Players.Find(id);
            if (p != null)
            {
                p.Name = name;
                _context.SaveChanges();
                return true;
            }

            return false;

        }
    }
}

using NavalWar.DAL.Models;
using NavalWar.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.DAL.Repositories
{
    public interface IGameRepository
    {
        public MapDTO CreateMap(int line, int column, int idGame, int idInGame, int idPlayer);
        public GameDTO CreateGame();
        public bool DeleteGame(int id);
        public GameDTO GetGame(int id);
        public bool PutShip(int gameID, int numPlayer, int numShip, int line, int column, Orientation orientation);
        public int Target(int gameID, int numPlayer, int line, int column);
        public IQueryable<Game> IncludeAll(IQueryable<Game> query);
    }
}

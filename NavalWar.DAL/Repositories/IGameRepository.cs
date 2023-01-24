﻿using NavalWar.DAL.Models;
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
        public MapDTO CreateMap(int line, int column, int idInGame, int idPlayer);
        public GameDTO CreateGame();
    }
}

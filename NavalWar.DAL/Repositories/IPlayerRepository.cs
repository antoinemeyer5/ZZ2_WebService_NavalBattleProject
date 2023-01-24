﻿using NavalWar.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.DAL.Repositories
{
    public interface IPlayerRepository
    {
        public PlayerDTO GetPlayer(int id);
        public PlayerDTO CreatePlayer(string name);
    }
}

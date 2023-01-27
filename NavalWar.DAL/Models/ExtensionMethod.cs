using Microsoft.EntityFrameworkCore.Query;
using NavalWar.DAL.Models;
using NavalWar.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethod
{
    static class ExtensionMethod
    {
        public static PlayerDTO toDTO(this Player player)
        {

            PlayerDTO p = new PlayerDTO();
            p.Id = player.Id;
            p.Name = player.Name;
            //p.History = player.History; // getGameForId

            return p;
        }

        public static GameDTO toDTO(this Game game)
        {

            GameDTO g = new GameDTO();
            g.IdGame= game.IdGame;
            g.WinnerName = game.WinnerName;
            g.Duration= game.Duration;
            g.Result = game.Result;
            g.ListMap[0] = game.Map0 is null ? null :game.Map0.toDTO();
            g.ListMap[1] = game.Map1 is null ? null : game.Map1.toDTO();

            return g;
        }

        // We do not need this function anymore, right ?
        public static MapDTO toDTO(this Map map)
        {

            MapDTO m = new MapDTO(map.Line, map.Column);
            m.AssociatedPlayer = map._associatedPlayer.toDTO();

            m.Body = map.Body;/* new List<List<int>>();

            List<int> l = map.Body.Split("|").Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse).ToList();
            m.Body = new List<List<int>>();
            for(int i = 0; i < map.Line;i++)
            {
                m.Body.Add(new List<int>());
                for(int j = 0; j < map.Column;j++)
                {
                    m.Body[i].Add(l[i*map.Line+j]);
                }
            }*/

            return m;
        }
    }
}

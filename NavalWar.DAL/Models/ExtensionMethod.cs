using Microsoft.EntityFrameworkCore.Query;
using NavalWar.DAL.Models;
using NavalWar.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
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
            g.IdGame = game.IdGame;
            g.WinnerId = game.WinnerId;
            g.Duration = game.Duration;
            g.Result = game.Result;
            g.ListMap[0] = game.Map0 is null ? null :game.Map0.toDTO();
            g.ListMap[1] = game.Map1 is null ? null : game.Map1.toDTO();

            return g;
        }

        public static MapDTO toDTO(this Map map)
        {

            MapDTO m = new MapDTO(map.Line, map.Column);
            m.AssociatedPlayer = map._associatedPlayer.toDTO();

            m.Body = JsonSerializer.Deserialize<List<List<int>>>(map.Body, (JsonSerializerOptions)null);
            return m;
        }
    }
}

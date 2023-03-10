using Microsoft.EntityFrameworkCore.Query;
using NavalWar.DAL.Models;
using NavalWar.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExtensionMethod
{
    public class VectorConverter : JsonConverter<Vector2>
    {
        public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            float x = 0;
            float y = 0;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return new Vector2(x, y);
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                string propertyName = reader.GetString();

                if (!reader.Read())
                {
                    throw new JsonException();
                }

                switch (propertyName)
                {
                    case "X":
                        x = reader.GetSingle();
                        break;
                    case "Y":
                        y = reader.GetSingle();
                        break;
                    default:
                        throw new JsonException();
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Vector2 value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("X", value.X);
            writer.WriteNumber("Y", value.Y);
            writer.WriteEndObject();
        }
    }

    static class ExtensionMethod
    {
        public static PlayerDTO toDTO(this Player player)
        {

            PlayerDTO p = new PlayerDTO();
            p.Id = player.Id;
            p.Name = player.Name;

            return p;
        }

        public static GameDTO toDTO(this Game game)
        {

            GameDTO g = new GameDTO();
            g.IdGame = game.IdGame;
            g.WinnerId = game.WinnerId;
            g.Duration = game.Duration;
            g.Result = game.Result;
            g.TourA = game.TourA;
            Console.WriteLine("Game: " + game.Map0?.ToString() + game.Map1?.ToString());
            g.ListMap[0] = game.Map0 == null ? null :game.Map0.toDTO();
            g.ListMap[1] = game.Map1 == null ? null : game.Map1.toDTO();

            return g;
        }

        public static MapDTO toDTO(this Map map)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new VectorConverter());
            MapDTO m = new MapDTO(map.Line, map.Column);
            Console.WriteLine(m.AssociatedPlayer?.Name);
            m.AssociatedPlayer = map._associatedPlayer.toDTO();
            Console.WriteLine(map.ListTarget);
            if (map.ListTarget != string.Empty)
                m.ListTarget = JsonSerializer.Deserialize<HashSet<Vector2>>(map.ListTarget, options);

            foreach(Vector2 v in m.ListTarget)
            {
                Console.WriteLine(v.ToString());
            }

            if (map.Body != string.Empty)
                m.Body = JsonSerializer.Deserialize<List<List<int>>>(map.Body, (JsonSerializerOptions)null);
            
            return m;
        }

    }
}

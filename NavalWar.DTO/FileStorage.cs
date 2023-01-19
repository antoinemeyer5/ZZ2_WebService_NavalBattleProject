using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace NavalWar.DTO
{
    

    public class FileStorage
    {
        static string filePath = @"database.json";

        public static void SaveGame(Game game)
        {
            List<Game> list = LoadGame();
            list.Add(game);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                JsonSerializer.Serialize<List<Game>>(fs, list);
                fs.Close();
            }
        }

        public static List<Game> LoadGame()
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                List<Game> list = null;
                try
                {
                    list = JsonSerializer.Deserialize<List<Game>>(fs);
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                { 
                    fs.Close();
                   
                }
                return list;
            }
        }

        public static Game GetGame(int id)
        {
            List<Game> list = LoadGame();
            return list.Find(elt => elt.idGame == id);
        }
    }

}

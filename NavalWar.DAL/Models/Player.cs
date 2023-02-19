using NavalWar.DTO;
using System.ComponentModel.DataAnnotations;

namespace NavalWar.DAL.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public  string Name { get; set; }

        public Player()
        {
            Name = string.Empty;
        }

        public Player(string name)
        {
            Name = name;
        }

    }
}
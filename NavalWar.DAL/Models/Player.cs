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
        //public List<Game> History { get; }
        ///=> class extensions

    }
}
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace BasketballPlayerApi.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? City { get; set; }

        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}

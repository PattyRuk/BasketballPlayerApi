using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace BasketballPlayerApi.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string City { get; set; } = string.Empty;

        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}

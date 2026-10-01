using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballPlayerApi.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Position { get; set; } // Guard, Forward, Center

        public int TeamId { get; set; }

        [ForeignKey(nameof(TeamId))]
        public Team? Team { get; set; }

        // Navigation for Many-to-Many Relationship
        public ICollection<PlayerGameStat> GameStats { get; set; } = new List<PlayerGameStat>();
    }
}

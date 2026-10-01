using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballPlayerApi.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime GameDate { get; set; }

        public int HomeTeamId { get; set; }
        [ForeignKey(nameof(HomeTeamId))]
        public Team? HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        [ForeignKey(nameof(AwayTeamId))]
        public Team? AwayTeam { get; set; }

        public ICollection<PlayerGameStat> PlayerStats { get; set; } = new List<PlayerGameStat>();
    }
}

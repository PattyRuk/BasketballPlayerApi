using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballPlayerApi.Models
{
    public class PlayerGameStat
    {
        [Key]
        public int Id { get; set; }

        public int PlayerId { get; set; }
        [ForeignKey(nameof(PlayerId))]
        public Player? Player { get; set; }

        public int GameId { get; set; }
        [ForeignKey(nameof(GameId))]
        public Game? Game { get; set; }

        public int Points { get; set; }

        public int Rebounds { get; set; }

        public int Assists { get; set; }
    }
}

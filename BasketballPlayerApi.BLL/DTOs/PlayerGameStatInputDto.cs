using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballPlayerApi.BLL.DTOs
{
    public class PlayerGameStatInputDto
    {
        [Required, Range(1, int.MaxValue)]
        public int PlayerId { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int GameId { get; set; }
        public int Points { get; set; }
        public int Rebounds { get; set; }
        public int Assists { get; set; }
    }
}

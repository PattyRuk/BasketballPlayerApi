using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballPlayerApi.BLL.DTOs
{
    public class TeamLeaderboardDto
    {
        public string? TeamName { get; set; }
        public int TotalPointsScored { get; set; }
        public double AveragePointsPerGame { get; set; }
    }
}

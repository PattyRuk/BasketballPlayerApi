using BasketballPlayerApi.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballPlayerApi.BLL.Services
{
    public interface ILeagueService
    {
        Task<PlayerOutputDto> CreatePlayerAsync(PlayerInputDto input);
        Task<PlayerOutputDto?> GetPlayerByIdAsync(int id);
        Task<IEnumerable<PlayerOutputDto>> GetPlayersPagedAsync(int pageNumber, int pageSize, string? search, string? position);
        Task<bool> UpdatePlayerAsync(int id, PlayerInputDto input);
        Task<bool> DeletePlayerAsync(int id);
        Task<bool> RecordStatsAsync(PlayerGameStatInputDto stats);
        Task<IEnumerable<TeamLeaderboardDto>> GetTeamLeaderboardAsync();
    }
}

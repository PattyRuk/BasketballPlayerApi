using BasketballPlayerApi.BLL.DTOs;
using BasketballPlayerApi.DAL;
using BasketballPlayerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballPlayerApi.BLL.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly LeagueDbContext _context;

        public LeagueService(LeagueDbContext context)
        {
            _context = context;
        }

        public async Task<PlayerOutputDto> CreatePlayerAsync(PlayerInputDto input)
        {
            var teamExists = await _context.Teams.AnyAsync(t => t.Id == input.TeamId);
            if (!teamExists) throw new ArgumentException($"Team ID {input.TeamId} does not exist.");

            var player = new Player
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Position = input.Position,
                TeamId = input.TeamId
            };

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            // Fetch back with team data attached for output mapping
            var savedPlayer = await _context.Players.Include(p => p.Team).FirstAsync(p => p.Id == player.Id);
            return MapToOutputDto(savedPlayer);
        }

        public async Task<PlayerOutputDto?> GetPlayerByIdAsync(int id)
        {
            var player = await _context.Players.Include(p => p.Team).FirstOrDefaultAsync(p => p.Id == id);
            return player == null ? null : MapToOutputDto(player);
        }

        // Multi-Field Filtering, Search, and Pagination Endpoint Logic
        public async Task<IEnumerable<PlayerOutputDto>> GetPlayersPagedAsync(int pageNumber, int pageSize, string? search, string? position)
        {
            var query = _context.Players.Include(p => p.Team).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(p => p.FirstName.ToLower().Contains(search) || p.LastName.ToLower().Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(position))
            {
                query = query.Where(p => p.Position.Equals(position, StringComparison.OrdinalIgnoreCase));
            }

            var list = await query
                .OrderBy(p => p.LastName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return list.Select(MapToOutputDto);
        }

        public async Task<bool> UpdatePlayerAsync(int id, PlayerInputDto input)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) return false;

            var teamExists = await _context.Teams.AnyAsync(t => t.Id == input.TeamId);
            if (!teamExists) throw new ArgumentException($"Team ID {input.TeamId} does not exist.");

            player.FirstName = input.FirstName;
            player.LastName = input.LastName;
            player.Position = input.Position;
            player.TeamId = input.TeamId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePlayerAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) return false;

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RecordStatsAsync(PlayerGameStatInputDto stats)
        {
            var playerExists = await _context.Players.AnyAsync(p => p.Id == stats.PlayerId);
            var gameExists = await _context.Games.AnyAsync(g => g.Id == stats.GameId);
            if (!playerExists || !gameExists) return false;

            var record = new PlayerGameStat
            {
                PlayerId = stats.PlayerId,
                GameId = stats.GameId,
                Points = stats.Points,
                Rebounds = stats.Rebounds,
                Assists = stats.Assists
            };

            _context.PlayerGameStats.Add(record);
            await _context.SaveChangesAsync();
            return true;
        }

        // Aggregation Query combining statistics across multiple entities
        public async Task<IEnumerable<TeamLeaderboardDto>> GetTeamLeaderboardAsync()
        {
            return await _context.Teams
                .Select(t => new TeamLeaderboardDto
                {
                    TeamName = t.Name,
                    TotalPointsScored = t.Players
                        .SelectMany(p => p.GameStats)
                        .Sum(s => s.Points),
                    AveragePointsPerGame = t.Players
                        .SelectMany(p => p.GameStats)
                        .Average(s => (double?)s.Points) ?? 0.0
                })
                .OrderByDescending(l => l.TotalPointsScored)
                .ToListAsync();
        }

        // manual mapping method
        private static PlayerOutputDto MapToOutputDto(Player p) => new()
        {
            Id = p.Id,
            FullName = $"{p.FirstName} {p.LastName}",
            Position = p.Position,
            TeamName = p.Team?.Name ?? "Free Agent"
        };
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;

namespace cps430_project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private AppDbContext _context;
        public TeamController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IEnumerable<Team> GetAll(string? query)
        {
            return _context.Teams
                .Include(t => t.Players)
                .Where(t => String.IsNullOrEmpty(query) ? true : (t.Name.ToLower().Contains(query.ToLower())))
                .OrderBy(t => t.Name)                
                .Select(x => new Team
                {
                    Id = x.Id,
                    Name = x.Name,
                    Wins = x.Wins,
                    Losses = x.Losses,
                    Draws = x.Draws,
                    MatchesPlayed = x.MatchesPlayed,
                    GoalCount = x.GoalCount
                })
                .ToList();
        }

        [HttpGet("{id}")]
        public Team? Get(int id)
        {
            return _context.Teams
                .Include(x => x.Coaches)
                .FirstOrDefault(x => x.Id == id);
        }

        [HttpGet("{id}/players")]
        public List<Player> GetPlayers(int id)
        {
            return _context.Players
                .Where(x => x.TeamId == id)
                .Include(p => p.Position)
                .ToList();
        }

        [HttpGet("top-performers")]  
        public List<Player> GetTopPerformers()
        {
            return _context.Players
                .OrderByDescending(x => x.Stats.Goals)
                .ThenByDescending(x => x.Stats.Assists)
                .ThenBy(x => x.Stats.YellowCards)
                .ThenBy(x => x.Stats.RedCards)
                .Take(4)
                .AsNoTracking()
                .Select(x => new Player
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Stats = x.Stats,
                })
                .ToList();
        }

        [HttpGet("player/{id}")]
        public Player GetPlayer(int id)
        {
            return _context.Players
                .Include(p => p.Team)
                .Include(p => p.PlayerMatches)
                .ThenInclude(p => p.Match)
                .Include("PlayerMatches.Match.HomeTeam")
                .Include("PlayerMatches.Match.AwayTeam")
                .Include(p => p.Position)
                .FirstOrDefault(p => p.Id == id);
        }

        [HttpGet("exclusive-losers")]
        public List<Team> GetExclusiveLosers()
        {
            return _context.Teams
                .Where(t => t.Wins == 0 && t.Draws == 0)
                .Select(x => new Team
                {
                    Name = x.Name,
                    GoalCount = x.GoalCount
                })
                .ToList();
        }

        [HttpGet("winners")]
        public List<Team> GetWinners()
        {
            return _context.Teams
                .Where(t => t.Wins > t.Losses)
                .OrderByDescending(t => t.Wins)
                .ThenByDescending(t => t.GoalCount)
                .Select(x => new Team
                {
                    Name = x.Name,
                    GoalCount = x.GoalCount,
                    Wins = x.Wins
                })
                .ToList();
        }

        public List<Player> GetScoringPlayers()
        {
            return _context.Players
                .Include(p => p.PlayerMatches)
                .Where(p => p.PlayerMatches.Sum(x => x.GoalsScored) > 0)
                .ToList();
        }

        public Team? GetMostConcededGoals()
        {
            return _context.Teams
                .Include(t => t.AwayMatches)
                .ThenInclude(t => t.PlayerMatches)
                .ThenInclude(t => t.Player)
                .Include(t => t.HomeMatches)
                .ThenInclude(t => t.PlayerMatches)
                .ThenInclude(t => t.Player)
                .OrderByDescending(t => t.AwayMatches
                        .Sum(x => x.PlayerMatches
                                .Where(p => p.Player.TeamId != t.Id)
                                .Sum(y => y.GoalsScored)))
                .ThenByDescending(t => t.HomeMatches
                        .Sum(x => x.PlayerMatches
                                .Where(p => p.Player.TeamId != t.Id)
                                .Sum(y => y.GoalsScored)))
                .FirstOrDefault();
        }
    }
}

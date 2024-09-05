using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Team
    {
        public int Id { get; set; }
        public int GoalCount { get; set; }
        public int MatchesPlayed { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public List<Coach> Coaches { get; set; }
        public List<Player> Players { get; set; }
        [InverseProperty(nameof(Match.HomeTeam))]
        public List<Match> HomeMatches { get; set; }
        [InverseProperty(nameof(Match.AwayTeam))]
        public List<Match> AwayMatches { get; set; }
        [InverseProperty(nameof(Match.WinningTeam))]
        public List<Match> WinningMatches { get; set; }
    }
}

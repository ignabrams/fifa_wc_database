using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Match
    {
        public int Id { get; set; }
        public virtual Team HomeTeam { get; set; }
        [ForeignKey(nameof(HomeTeam))]
        public int? HomeTeamId { get; set; }
        public virtual Team AwayTeam { get; set; }
        [ForeignKey(nameof(AwayTeam))]
        public int? AwayTeamId { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public virtual Team? WinningTeam { get; set; }
        [ForeignKey(nameof(WinningTeam))]
        public int? WinningTeamId { get; set; }
        public IEnumerable<PlayerMatch> PlayerMatches { get; set; }
    }
}

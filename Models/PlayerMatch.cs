using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PlayerMatch
    {
        public Player Player { get; set; }
        public int PlayerId { get; set; }
        public Match Match { get; set; }
        public int MatchId { get; set; }
        public short GoalsScored { get; set; }
    }
}

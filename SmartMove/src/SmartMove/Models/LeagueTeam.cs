using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMove.Models
{
    public class LeagueTeam
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int LeagueId { get; set; }
        public League League { get; set; }
    }
}

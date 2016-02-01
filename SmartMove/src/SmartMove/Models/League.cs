using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMove.Models
{
    public class League
    {
        public int LeagueId { get; set; }
        public string NameLeague { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public List<LeagueTeam> LeagueTeams{ get; set; }
    }
}

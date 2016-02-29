using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMove.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public String Name { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<Match> HomeMatches { get; set; }
        public virtual ICollection<Match> AwayMatches { get; set; }
        public virtual ICollection<Match> WinnerMatches { get; set; }
        

        public List<LeagueTeam> LeagueTeams { get; set; }
    }

}

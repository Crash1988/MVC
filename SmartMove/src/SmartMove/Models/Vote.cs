using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMove.Models
{
    public enum VotedTeam
    {
        AwayTeam,
        HomeTeam
    }

    public class Vote
    {
        public int VoteId { get; set; }
        public DateTime datevote { get; set; }
        public Match Match { get; set; }
        public VotedTeam VotedTeam { get; set; }
        
        public string ApplicationUserID { get; set; }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartMove.Models;

namespace SmartMove.ViewModels.vote
{
    public class VoteMatch
    {
        public List<Match> UnVotedMatches { get; set; }
        public List<Vote> Votes { get; set; }
        public VoteMatch(string userid) {
            ApplicationDbContext db = new ApplicationDbContext();
            this.Votes = db.Vote.Where(v => v.user.Id == userid).ToList();
            this.UnVotedMatches = db.Match.Where(p => !Votes.Any(p2 => p2.Match.MatchId == p.MatchId)).ToList();


        }
    }
}

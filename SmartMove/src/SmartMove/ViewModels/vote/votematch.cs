using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartMove.Models;

namespace SmartMove.ViewModels.VoteMatch
{
    public class VoteMatch
    {
        public List<Match> UnVotedMatches { get; set; }
        public List<Vote> Votes { get; set; }
        private ApplicationDbContext db;
        public VoteMatch(ApplicationDbContext context,string userid)
        {
            this.db = context;

            this.Votes = db.Vote.Where(v => v.user.Id == userid).ToList();
            this.UnVotedMatches = db.Match.Where(p => !Votes.Any(p2 => p2.Match.MatchId == p.MatchId)).ToList();

        }
        public VoteMatch(List<Vote> votes, List<Match> unVotedMatches)
        {
            Votes = votes;
            UnVotedMatches = unVotedMatches;
        }
    }
}

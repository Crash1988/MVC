using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMove.Models
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }

        public DateTime playday { get; set; }
        public float HomePoints { get; set; }
        public float GuestPoints { get; set; }

        public int? HomeTeamId { get; set; }
        public int? GuestTeamId { get; set; }

        [ForeignKey("HomeTeamId")]
        [InverseProperty("HomeMatches")]
        public virtual Team HomeTeam { get; set; }

        [ForeignKey("GuestTeamId")]
        [InverseProperty("AwayMatches")]
        public virtual Team GuestTeam { get; set; }

    }

}

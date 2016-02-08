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
        public enum WinnerTeam
        {
            AwayTeam,
            HomeTeam,
            Draw
        }
        [Key]
        public int MatchId { get; set; }

        public DateTime PalyDay { get; set; }
        public float HomePoints { get; set; }
        public float GuestPoints { get; set; }
        
        public int? HomeTeamId { get; set; }
        public int? GuestTeamId { get; set; }
        public WinnerTeam? Result { get; set; }

        [ForeignKey("HomeTeamId")]
        [InverseProperty("HomeMatches")]
        public virtual Team HomeTeam { get; set; }

        [ForeignKey("GuestTeamId")]
        [InverseProperty("AwayMatches")]
        public virtual Team GuestTeam { get; set; }

    }

}

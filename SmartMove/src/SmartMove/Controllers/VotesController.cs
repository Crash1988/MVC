using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using SmartMove.Models;
using SmartMove.ViewModels.VoteMatch;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNet.Authorization;

namespace SmartMove.Controllers
{
    [Authorize]
    public class VotesController : Controller
    {
        private ApplicationDbContext _context;

        public VotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Votes
        public IActionResult Index()
        {
            string userId = User.GetUserId();
            var dbVotes = _context.Vote.Include(m => m.Match)
                .ThenInclude(v => v.HomeTeam)
                .Include(m => m.Match)
                .ThenInclude(v => v.GuestTeam);

            List<Vote> Votes = dbVotes.Where(v => v.user.Id == userId).ToList();
            List<Match> UnVotedMatches = _context.Match.Where(p => !Votes.Any(p2 => p2.Match.MatchId == p.MatchId))
                .Include(ht => ht.HomeTeam)
                .Include(gt => gt.GuestTeam)
                .ToList();
            VoteMatch VoteMatch = new VoteMatch(Votes, UnVotedMatches);


            return View(VoteMatch);
        }

        // GET: Votes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Vote vote = _context.Vote.Single(m => m.VoteId == id);
            if (vote == null)
            {
                return HttpNotFound();
            }

            return View(vote);
        }

        // GET: Votes/Details/5
        public IActionResult Create(int? id, int? teamid, int? matchid)
        {
            Match match = _context.Match.Single(m => m.MatchId == matchid);
            Vote vote = new Vote();
            vote.Match = match;
            string userId = User.GetUserId();
            vote.user = _context.Users.Single(u => u.Id == userId);
            Team team = _context.Team.Single(s => s.TeamId == teamid);
            vote.VotedTeam = team;
            _context.Vote.Add(vote);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Votes/Details/5
        public IActionResult Edit(int? id, int? teamid )
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            
                var votetmp = _context.Vote.Where(m => m.VoteId == id).Include(m => m.Match);
              Vote  vote = votetmp.Single(m => m.VoteId == id);
            

            if (vote == null)
            {
                return HttpNotFound();
            }

            Team team = _context.Team.Single(s => s.TeamId == teamid);
            vote.VotedTeam = team;
            _context.Vote.Update(vote);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        // GET: Votes/Create
        public IActionResult Createasd()
        {
            return View();
        }

        // POST: Votes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vote vote)
        {
            if (ModelState.IsValid)
            {
                _context.Vote.Add(vote);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vote);
        }

        // GET: Votes/Edit/5
        public IActionResult Editasd(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Vote vote = _context.Vote.Single(m => m.VoteId == id);
            if (vote == null)
            {
                return HttpNotFound();
            }
            return View(vote);
        }

        // POST: Votes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editasd(Vote vote)
        {
            if (ModelState.IsValid)
            {
                _context.Update(vote);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vote);
        }

        // GET: Votes/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Vote vote = _context.Vote.Single(m => m.VoteId == id);
            if (vote == null)
            {
                return HttpNotFound();
            }

            return View(vote);
        }

        // POST: Votes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Vote vote = _context.Vote.Single(m => m.VoteId == id);
            _context.Vote.Remove(vote);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

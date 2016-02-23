using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using SmartMove.Models;

namespace SmartMove.Controllers
{
    public class MatchesController : Controller
    {
        private ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Matches
        public IActionResult Index()
        {
            var applicationDbContext = _context.Match.Include(m => m.GuestTeam).Include(m => m.HomeTeam);
            return View(applicationDbContext.ToList());
        }

        // GET: Matches/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Match match = _context.Match.Single(m => m.MatchId == id);
            if (match == null)
            {
                return HttpNotFound();
            }
            Team ht = _context.Team.Single(t => t.TeamId == match.HomeTeamId);
            match.HomeTeam = ht;
            Team gt = _context.Team.Single(t => t.TeamId == match.GuestTeamId);
            match.GuestTeam = gt;

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["GuestTeamId"] = new SelectList(_context.Team, "TeamId", "Name");
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "TeamId", "Name");
            return View();
        }

        // POST: Matches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Match.Add(match);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["GuestTeamId"] = new SelectList(_context.Team, "TeamId", "GuestTeam", match.GuestTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "TeamId", "HomeTeam", match.HomeTeamId);
            return View(match);
        }
        //editing matches
        // GET: Matches/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Match match = _context.Match.Single(m => m.MatchId == id);
            if (match == null)
            {
                return HttpNotFound();
            }
            ViewData["GuestTeamId"] = new SelectList(_context.Team, "TeamId", "Name", match.GuestTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "TeamId", "Name", match.HomeTeamId);
            return View(match);
        }

        // POST: Matches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Update(match);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["GuestTeamId"] = new SelectList(_context.Team, "TeamId", "GuestTeam", match.GuestTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "TeamId", "HomeTeam", match.HomeTeamId);
            return View(match);
        }

        // GET: Matches/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Match match = _context.Match.Single(m => m.MatchId == id);
            if (match == null)
            {
                return HttpNotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Match match = _context.Match.Single(m => m.MatchId == id);
            _context.Match.Remove(match);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

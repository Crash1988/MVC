using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using SmartMove.Models;

namespace SmartMove.Controllers
{
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
            return View(_context.Vote.ToList());
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

        // GET: Votes/Create
        public IActionResult Create()
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
        public IActionResult Edit(int? id)
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
        public IActionResult Edit(Vote vote)
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

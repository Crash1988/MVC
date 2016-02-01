using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using SmartMove.Models;

namespace SmartMove.Controllers
{
    public class TeamsController : Controller
    {
        private ApplicationDbContext _context;

        public TeamsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Teams
        public IActionResult Index()
        {
            return View(_context.Team.ToList());
        }

        // GET: Teams/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Team team = _context.Team.Single(m => m.TeamId == id);
            if (team == null)
            {
                return HttpNotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Team.Add(team);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Team team = _context.Team.Single(m => m.TeamId == id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Update(team);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Team team = _context.Team.Single(m => m.TeamId == id);
            if (team == null)
            {
                return HttpNotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Team team = _context.Team.Single(m => m.TeamId == id);
            _context.Team.Remove(team);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

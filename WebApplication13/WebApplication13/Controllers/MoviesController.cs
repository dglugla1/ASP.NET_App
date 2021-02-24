using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication13.Data;
using WebApplication13.Models;

namespace WebApplication13.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public MoviesController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IQueryable<Movie> m = (from movies in _context.movies where movies.userId==userId select movies);
            var list = m.Select(m => (Movie)m).ToListAsync();
            return View(await list);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.movies
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["userId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Director,Year,Genre,userId")] Movie movie)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                movie.userId = userId;
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["userId"] = new SelectList(_context.Set<User>(), "Id", "Id", movie.userId);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["userId"] = new SelectList(_context.Set<User>(), "Id", "Id", movie.userId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Director,Year,Genre,userId")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["userId"] = new SelectList(_context.Set<User>(), "Id", "Id", movie.userId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.movies
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.movies.FindAsync(id);
            _context.movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.movies.Any(e => e.Id == id);
        }

        //GET: Movies/Oldest
        public async Task<IActionResult> Oldest()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IQueryable<Movie> m = (from movies in _context.movies where movies.userId == userId orderby movies.Year descending select movies).Take(2);
            var list = m.Select(m => (Movie)m).ToListAsync();
            return View("MyLists", await list);
        }

        //GET: Movies/Newest
        public async Task<IActionResult> Newest()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IQueryable<Movie> m = (from movies in _context.movies where movies.userId == userId orderby movies.Year select movies).Take(2);
            var list = m.Select(m => (Movie)m).ToListAsync();
            return View("MyLists", await list);
        }

        //GET: Movies/SearchYear
        public async Task<IActionResult> SearchYear(int year)
        {
            year = 1976;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IQueryable<Movie> m = (from movies in _context.movies where movies.userId == userId && movies.Year == year orderby movies.Year select movies).Take(2);
            var list = m.Select(m => (Movie)m).ToListAsync();
            return View("MyLists", await list);
        }

        //GET: Movies/SortedList
        public async Task<IActionResult> SortedList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IQueryable<Movie> m = (from movies in _context.movies where movies.userId == userId orderby movies.Title select movies);
            var list = m.Select(m => (Movie)m).ToListAsync();
            return View("SortedList", await list);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlayerManager.Context;
using PlayerManager.Models;

namespace PlayerManager.Controllers
{
    public class PlayersController : Controller
    {
        private readonly EfContext _context;

        public PlayersController(EfContext context)
        {
            _context = context;
        }

        // GET: Players
        public async Task<IActionResult> Index(
            string? SortOrder, Position? PositionFilter, string? NameFilter, bool? RetiredFilter
            )
        {
            var efContext = _context.Players.Include(p => p.ReferencedTeam);
            var res = efContext.AsQueryable();

            //Console.WriteLine(SortOrder + " " + PositionFilter + " " + NameFilter
            //    + " " + RetiredFilter);

            if(SortOrder != null && SortOrder.Equals("up"))
            {
                res = res.OrderByDescending(e => e.Name);
            }
            if (SortOrder != null && SortOrder.Equals("down"))
            {
                res = res.OrderBy(e => e.Name);
            }
            if (PositionFilter != null)
            {
                res = res.Where(e => e.Position == PositionFilter);
            }
            if (NameFilter != null)
            {
                res = res.Where(e => e.Name.ToLower().Contains(
                    NameFilter.ToLower()));
            }
            if (RetiredFilter != null)
            {
                res = res.Where(e => e.Retired == RetiredFilter);
            }

            return View(await res.ToListAsync());
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.ReferencedTeam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "TeamName");
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,Position,Retired,ContractExp,TeamId")] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "TeamName", player.TeamId);
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "TeamName", player.TeamId);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,Position,Retired,ContractExp,TeamId")] Player player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id))
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
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "TeamName", player.TeamId);
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.ReferencedTeam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.Id == id);
        }
    }
}

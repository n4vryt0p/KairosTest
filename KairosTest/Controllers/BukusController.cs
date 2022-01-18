using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KairosTest.Data;
using KairosTest.Entities;
using Microsoft.AspNetCore.Authorization;

namespace KairosTest.Controllers
{
    [Authorize]
    public class BukusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BukusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bukus
        public async Task<IActionResult> Index()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return View(await _context.Buku.ToListAsync());
        }

        // GET: Bukus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buku = await _context.Buku
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buku == null)
            {
                return NotFound();
            }

            return View(buku);
        }

        // GET: Bukus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bukus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JudulBuku,Pengarang,JenisBuku,HargaSewa")] Buku buku)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buku);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        // GET: Bukus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buku = await _context.Buku.FindAsync(id);
            if (buku == null)
            {
                return NotFound();
            }
            return View(buku);
        }

        // POST: Bukus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JudulBuku,Pengarang,JenisBuku,HargaSewa")] Buku buku)
        {
            if (id != buku.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buku);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BukuExists(buku.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return BadRequest();
        }

        // GET: Bukus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buku = await _context.Buku
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buku == null)
            {
                return NotFound();
            }

            return View(buku);
        }

        // POST: Bukus/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buku = await _context.Buku.FindAsync(id);
            _context.Buku.Remove(buku);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool BukuExists(int id)
        {
            return _context.Buku.Any(e => e.Id == id);
        }
    }
}

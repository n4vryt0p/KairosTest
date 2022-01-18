using KairosTest.Data;
using KairosTest.Entities;
using KairosTest.Handlers;
using KairosTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KairosTest.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost("sewa")]
        public async Task<ActionResult<ResultSewaBuku>> GetSewaBukuAsync([FromBody] DtViewModel search)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var results = new ResultSewaBuku
            {
                Draw = search.Draw,
                Data = new List<SewaBukuViewModel>()
            };
            var start = search.Start;
            var length = search.Length;
            var preSortColumn = search.Order.FirstOrDefault()?.Column;
            var sortColumn = search.Columns[preSortColumn ?? 0].Data;
            var sortColumnDir = search.Order.FirstOrDefault()?.Dir;
            var searchValue = search.Search.Value;

            int pageSize = length != null ? Convert.ToInt32(length) : 10;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            var dateNow = DateTime.UtcNow;
            var docData = _context.SewaBuku
                .Select(x => new SewaBukuViewModel
                {
                    Id = x.Id,
                    JudulBuku = x.Buku.JudulBuku,
                    Pengarang = x.Buku.Pengarang,
                    JenisBuku = x.Buku.JenisBuku,
                    JumlahHari = x.JumlahHari,
                    HargaSewa = x.Buku.HargaSewa,
                    TotalSewa = x.Buku.HargaSewa * x.JumlahHari,
                    UserId = x.IdentityUserId
                });

            if (!User.IsInRole("Admin"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                docData = docData.Where(t => t.UserId == userId);
            }

            results.RecordsTotal = docData.Count();

            //Search    
            if (!string.IsNullOrEmpty(searchValue))
                docData = docData.Where(m => m.JudulBuku.Contains(searchValue) || m.Pengarang.Contains(searchValue) ||
                                             m.JenisBuku.Contains(searchValue));

            //Sorting
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                docData = docData.OrderBy(sortColumn, sortColumnDir);

            results.RecordsFiltered = docData.Count();
            results.Data = await docData.Skip(skip).Take(pageSize).ToListAsync().ConfigureAwait(false);
            return Ok(results);
        }

        [HttpGet("BukuDdl")]
        public async Task<IEnumerable<Buku>> GetBuku()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.Buku.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> AddSewaAsync([Bind("BukuId,MulaiSewa,SelesaiSewa")] AddSewaBukuViewModel sewa)
        {
            if (sewa.BukuId < 1)
                return BadRequest();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dataSewa = new SewaBuku
            {
                MulaiSewa = sewa.MulaiSewa,
                SelesaiSewa = sewa.SelesaiSewa,
                BukuId = sewa.BukuId,
                IdentityUserId = userId
            };
            _context.Add(dataSewa);

            await _context.SaveChangesAsync();
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

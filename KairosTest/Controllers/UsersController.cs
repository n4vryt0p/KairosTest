using KairosTest.Data;
using KairosTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KairosTest.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UsersController
        public async Task<IActionResult> IndexAsync()
        {
            var appUser = _userManager.Users.AsNoTracking().AsAsyncEnumerable();
            var result = new List<UsersViewModel>();
            await foreach (var item in appUser)
            {
                result.Add(new UsersViewModel
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    Email = item.Email,
                    Role = _userManager.GetRolesAsync(item).Result.FirstOrDefault()
                });
            }
            return View(result);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(string id)
        {
            return View();
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Email,Password,Role")] UsersViewModel user)
        {
            if (ModelState.IsValid)
            {
                var use = new IdentityUser { 
                    UserName = user.UserName,
                    Email = user.Email,
                };
                var rrr = await _userManager.CreateAsync(use, user.Password);
                if (rrr.Succeeded)
                {
                    rrr = await _userManager.AddToRoleAsync(use, user.Role);
                    if (rrr.Succeeded)
                        return Ok();
                }
            }
            return BadRequest();
        }

        // GET: UsersController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            var result = new UsersViewModel
                {
                    Id = appUser.Id,
                    UserName = appUser.UserName,
                    Email = appUser.Email,
                    Role = _userManager.GetRolesAsync(appUser).Result.FirstOrDefault()
                };
            return View(result);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email,Password,Role")] UsersViewModel user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var use = new IdentityUser
                    {
                        UserName = user.UserName,
                        Email = user.Email
                    };
                    var rrr = await _userManager.UpdateAsync(use);
                    if (rrr.Succeeded)
                    {
                        await _userManager.ChangePasswordAsync(use,user.CurrentPassword, user.Password);
                        rrr = await _userManager.RemoveFromRolesAsync(use, new string[] { "Admin","Penyewa"});
                        if (rrr.Succeeded)
                        {
                            rrr = await _userManager.AddToRoleAsync(use, user.Role);
                            if (rrr.Succeeded)
                                return Ok();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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

        // GET: UsersController/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_23.Models;

namespace Lab_23.Controllers
{
    public class LoginController : Controller
    {
        private readonly ShopContext _context;

        public ShopContext DB = new ShopContext();
        public Users db = new Users();


        public LoginController(ShopContext context)
        {
            _context = context;
        }

        public IActionResult Logout()
        {
            return View();
        }


        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserLogin(Users FormUser)
        {
            Users dbu = _context.Users.ToList().Find(u => u.UserName == FormUser.UserName);

            if (dbu == null)
            {
                ViewBag.Error = "Invalide Username or Password";
                return View();
            }
            else if (FormUser.Password == dbu.Password)
            {
                TempData["UserName"] = FormUser.UserName;
                ViewBag.Name = FormUser.UserName;
                return RedirectToAction("Products", controllerName: "Shop");
            }
            else
            {
                return RedirectToAction("UserLogin");
            }
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,UserName,Password,Money")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(UserLogin));
            }
            return View(users);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(UserLogin));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}

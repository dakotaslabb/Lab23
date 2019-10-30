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
    public class Shop : Controller
    {
        private readonly ShopContext _context;

        public Shop(ShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Products()
        {
            return View(await _context.Products.ToListAsync());
        }

        public IActionResult Buy(int id)
        {
            Products p = _context.Products.Find(id);

            if (p != null)
            {
                return View(p);
            }
            else
            {
                RedirectToAction("Products");
            }
            return View();
        }

        public IActionResult Recipte(int id)
        {
            Users user = _context.Users.ToList().Find(u => u.UserName == TempData.Peek("UserName") as string);
            Products PurchasedProduct = _context.Products.Find(id);

            if (user.Money > PurchasedProduct.Price)
            {
                ViewBag.Purchase = "Thanks For Purchasing our Product";
                user.Money -= PurchasedProduct.Price;
                _context.SaveChanges();
                return View(PurchasedProduct);
            }
            else
            {
                ViewBag.Error = "You do not have enough money to purchase this.";
               return View();
            }
        }
    }
}

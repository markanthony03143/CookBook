using CookBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CookBook.Controllers
{
    [RequireLoggedOut]
    public class AccountController : Controller
    {
        private readonly CookBookDbContext _context;

        public AccountController(CookBookDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            ModelState.Clear();

            var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);

            if (existingUser != null && existingUser.Password == user.Password)
            {
                // Set session variables upon successful login
                HttpContext.Session.SetString("UserId", existingUser.UserId.ToString());
                HttpContext.Session.SetString("Username", existingUser.Username);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid email or password";
                return View(user);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                string ppurl = "https://ui-avatars.com/api/?name=" + user.Username.ToString();
                user.ProfilePicUrl = ppurl;
                _context.Users.Add(user);
                _context.SaveChanges();
                ViewBag.Message = "Successfully Registered";
                return View("Login");
            }

            return View(user);
        }
    }
}

using IOFA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IOFA.Controllers
{
    public class InstituteController : Controller
    {
        private readonly YourDbContext _context;

        public InstituteController(YourDbContext context)
        {
            _context = context;
        }



        public IActionResult Index()
        {
            return View();
        }


        public IActionResult EmergingTalent()
        {
            return View();
        }

        public IActionResult Artist()
        {
            var artworks = _context.Arts.ToList();

            var studentEmails = artworks
                .Where(a => !string.IsNullOrEmpty(a.UploadedBy))
                .Select(a => a.UploadedBy)
                .Distinct()
                .ToList();

            var students = _context.Registers
                .Where(r => studentEmails.Contains(r.Email) && r.Email != null)
                .ToDictionary(r => r.Email!, r => r.Name);

            ViewBag.StudentNames = students;

            return View(artworks);
        }

        public IActionResult Press()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Space()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SpaceRegister(Register user)
        {
            if (ModelState.IsValid)
            {
                _context.Registers.Add(user);
                await _context.SaveChangesAsync();

                // Set session for Email and Name
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetString("Name", user.Name);
                HttpContext.Session.SetString("Phone", user.Phone);
                HttpContext.Session.SetString("CreatedDate", user.CreatedDate.ToString());

                return RedirectToAction("Space");
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginUser(string Email, string Password)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                TempData["Error"] = "Email aur Password zaroori hain.";
                return RedirectToAction("Space");
            }

            var user = _context.Registers
                .FirstOrDefault(u => u.Email == Email && u.Password == Password);

            if (user != null)
            {
                TempData["Success"] = "Login successful. Welcome " + user.Name + "!";

                // ✅ Yahan bas yehi 2 consistent keys use karo
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetString("Name", user.Name);
                HttpContext.Session.SetString("Phone", user.Phone);
                HttpContext.Session.SetString("CreatedDate", user.CreatedDate.ToString());

                return RedirectToAction("Home", "Student");
            }
            else
            {
                TempData["Error"] = "Email ya Password galat hai.";
                return RedirectToAction("Space");
            }
        }

        





        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Exhibition()
        {
            var exhibitions = _context.Exhibitions.ToList();
            return View(exhibitions);
        }



    }
 }


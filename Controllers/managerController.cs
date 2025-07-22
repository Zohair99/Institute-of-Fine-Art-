using IOFA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace managerdashboard1.Controllers
{
    public class managerController : Controller
    {

        private readonly YourDbContext _context;
        private readonly IWebHostEnvironment _env;

        public managerController(YourDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            string name = HttpContext.Session.GetString("ManagerName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("ManagerRegister", "manager");
            }
            return View();
        }

        public IActionResult managerhome()
        {
            string name = HttpContext.Session.GetString("ManagerName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("ManagerRegister", "manager");
            }
            return View();
        }

        public IActionResult arts()
        {
            string name = HttpContext.Session.GetString("ManagerName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("ManagerRegister", "manager");
            }
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
        public IActionResult upcomingexhibitions()
        {
            string name = HttpContext.Session.GetString("ManagerName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("ManagerRegister", "manager");
            }
            var exhibitions = _context.Exhibitions.ToList();
            return View(exhibitions);
        }

        public IActionResult exhibition()
        {
            string name = HttpContext.Session.GetString("ManagerName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("ManagerRegister", "manager");
            }
            var exhibitions = _context.Exhibitions.ToList();
            return View(exhibitions);
        }

        public IActionResult competition()
        {
            string name = HttpContext.Session.GetString("ManagerName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("ManagerRegister", "manager");
            }
            var competitions = _context.Compititions.ToList(); // Competition table ka data fetch
            return View(competitions); // view me bhejna
        }

        public IActionResult managerprofile()
        {
            string name = HttpContext.Session.GetString("ManagerName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("ManagerRegister", "manager");
            }
            return View();
        }

        public IActionResult addexhibition()
        {

            return View();
        }

        [HttpGet]
        public IActionResult AddExhibition()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddExhibition(Exhibition exhibition, IFormFile ImageFile)
        {
            try
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    exhibition.Pic = "/uploads/" + uniqueFileName;
                }

                _context.Exhibitions.Add(exhibition);
                await _context.SaveChangesAsync();

                return RedirectToAction("UpcomingExhibitions");
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError("", "Error: " + inner);
            }

            return View(exhibition);


        }

        public IActionResult DeleteExhibition(int id)
        {
            var exhibition = _context.Exhibitions.Find(id);
            if (exhibition != null)
            {
                _context.Exhibitions.Remove(exhibition);
                _context.SaveChanges();
            }

            return RedirectToAction("UpcomingExhibitions");
        }







        public IActionResult editmanagerprofile()
        {
            return View();
        }

        public IActionResult ManagerRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ManagerRegister(Manager model)
        {
            if (ModelState.IsValid)
            {
                _context.Managers.Add(model);
                await _context.SaveChangesAsync();

                // Safe Set Session
                HttpContext.Session.SetString("Email", model.Email ?? "");
                HttpContext.Session.SetString("Name", model.Name ?? "");
                HttpContext.Session.SetString("Phone", model.Phone ?? "");
                HttpContext.Session.SetString("CreatedDate", model.CreatedDate.ToString());

                return RedirectToAction("ManagerRegister");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManagerLogin()
        {
            // 1. Get values from form
            string email = Request.Form["Email"];
            string password = Request.Form["Password"];

            // 2. Validate empty values
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Email and password are required.";
                return View(); // Make sure the View exists
            }

            // 3. Check against DB
            var user = await _context.Managers
                .FirstOrDefaultAsync(M => M.Email == email && M.Password == password);

            // 4. If found, set session and redirect
            if (user != null)
            {
                HttpContext.Session.SetString("ManagerEmail", user.Email);
                HttpContext.Session.SetString("ManagerName", user.Name ?? "Manager");
                HttpContext.Session.SetString("ManagerPhone", user.Phone ?? "Manager");
                HttpContext.Session.SetString("CreatedDate", user.CreatedDate.ToString());

                return RedirectToAction("managerhome", "manager");
            }

            // 5. If invalid credentials
            ViewBag.Error = "Invalid email or password.";
            return View(); // Show error message on login page
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("ManagerRegister");
        }

        

    }
}

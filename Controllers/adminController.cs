using IOFA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace admindash1.Controllers
{
    public class adminController : Controller
    {
        private readonly YourDbContext _context;
        private readonly IWebHostEnvironment _env;

        public adminController(YourDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            string name = HttpContext.Session.GetString("AdminName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("AdminRegister", "admin");
            }
            return View();
        }


        public IActionResult adminhome()
        {
            string name = HttpContext.Session.GetString("AdminName");

            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("AdminRegister", "admin");
            }

            ViewBag.Students = _context.Registers.ToList();
            ViewBag.Teachers = _context.Teachers.ToList();

            return View();
        }



        public IActionResult Students()
        {
            string name = HttpContext.Session.GetString("AdminName");

            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("AdminRegister", "admin");
            }

            // Fetch all students (assuming 'Registers' is your student table)
            var students = _context.Registers.ToList();

            return View(students);

        }


        public IActionResult teachers()
        {
            string name = HttpContext.Session.GetString("AdminName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("AdminRegister", "admin");
            }

            var teachers = _context.Teachers.ToList(); // fetch from DB
            return View(teachers);
        }


        public IActionResult profile()
        {
            string name = HttpContext.Session.GetString("AdminName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("AdminRegister", "admin");
            }
            return View();
        }

        public IActionResult addstudent()
        {
            return View();
        }

        public IActionResult editstudent()
        {
            return View();
        }

        public IActionResult viewstudent()
        {
            return View();
        }

        public IActionResult addteacher()
        {
            return View();
        }

        public IActionResult editteacher()
        {
            return View();
        }

        public IActionResult viewteacher()
        {
            return View();
        }

        public IActionResult editprofile()
        {
            return View();
        }

        public IActionResult artwork()
        {
            var artworks = _context.Arts.ToList();

            // Get distinct non-empty emails from artworks
            var studentEmails = artworks
                .Where(a => !string.IsNullOrEmpty(a.UploadedBy))
                .Select(a => a.UploadedBy)
                .Distinct()
                .ToList();

            // Fetch matching names from Registers table
            var students = _context.Registers
                .Where(r => studentEmails.Contains(r.Email) && r.Email != null)
                .ToDictionary(r => r.Email!, r => r.Name);

            ViewBag.StudentNames = students;

            return View(artworks);
        }


        public IActionResult AdminRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminRegister(Admin model)
        {
            if (ModelState.IsValid)
            {
                _context.Admins.Add(model);
                await _context.SaveChangesAsync();

                // Safe Set Session
                HttpContext.Session.SetString("Email", model.Email ?? "");
                HttpContext.Session.SetString("Name", model.Name ?? "");
                HttpContext.Session.SetString("Phone", model.Phone ?? "");
                HttpContext.Session.SetString("CreatedDate", model.CreatedDate.ToString());

                return RedirectToAction("AdminRegister");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin()
        {
            string email = Request.Form["Email"];
            string password = Request.Form["Password"];

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Email and password are required.";
                return View("AdminRegister");
            }

            var user = await _context.Admins
                .FirstOrDefaultAsync(A => A.Email == email && A.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("AdminEmail", user.Email);
                HttpContext.Session.SetString("AdminName", user.Name ?? "Admin");
                HttpContext.Session.SetString("AdminPhone", user.Phone ?? "Admin");

                return RedirectToAction("adminhome", "admin");
            }

            ViewBag.Error = "Invalid email or password.";
            return View("AdminRegister");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("AdminRegister");
        }

        [HttpPost]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Registers.Find(id);

            if (student != null)
            {
                _context.Registers.Remove(student);
                _context.SaveChanges();
            }

            return RedirectToAction("Students");
        }

        [HttpPost]
        public IActionResult DeleteTeacher(int id)
        {
            var teacher = _context.Teachers.Find(id);

            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                _context.SaveChanges();
            }

            return RedirectToAction("Teachers");
        }




    }
}

using IOFA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace teacher1.Controllers
{
    public class teacherController : Controller
    {
        private readonly YourDbContext _context;
        private readonly IWebHostEnvironment _env;

        public teacherController(YourDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();  
        }

        public IActionResult Home1()
        {
            string name = HttpContext.Session.GetString("TeacherName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("TeacherRegister", "Teacher");
            }

            // Send data to view (via ViewBag or model)
            ViewBag.TeacherName = name;
            return View();
        }



        public IActionResult manage()
        {
            string name = HttpContext.Session.GetString("TeacherName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("TeacherRegister", "Teacher");
            }

            var competitions = _context.Compititions.ToList(); // Competition table ka data fetch
            return View(competitions); // view me bhejna
        }


        public IActionResult addcompetition()
        {

            string name = HttpContext.Session.GetString("TeacherName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("TeacherRegister", "Teacher");
            }

            return View();
        }

        [HttpPost]
        public IActionResult SaveCompetition(Compitition model)
        {
            _context.Compititions.Add(model);
            _context.SaveChanges();

            HttpContext.Session.SetString("Notification", "Competition added successfully!");


            return RedirectToAction("AddCompetition");
        }


        // GET: editcompetition
        [HttpGet]
        public IActionResult editcompetition(int id)
        {
            var competition = _context.Compititions.FirstOrDefault(c => c.Id == id);
            if (competition == null)
            {
                return NotFound();
            }
            return View(competition);
        }

        [HttpPost]
        public IActionResult editcompetition(Compitition model)
        {
            if (ModelState.IsValid)
            {
                var existing = _context.Compititions.FirstOrDefault(c => c.Id == model.Id);
                if (existing != null)
                {
                    existing.Title = model.Title;
                    existing.Theme = model.Theme;
                    existing.Description = model.Description;
                    existing.SubmitDate = model.SubmitDate;

                    _context.SaveChanges();
                    return RedirectToAction("manage");
                }
            }
            return View(model);
        }

        public IActionResult DeleteCompetition(int id)
        {
            var competition = _context.Compititions.Find(id);
            if (competition != null)
            {
                _context.Compititions.Remove(competition);
                _context.SaveChanges();
            }

            return RedirectToAction("manage");
        }







        public IActionResult Awards()
        {
            string name = HttpContext.Session.GetString("TeacherName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("TeacherRegister", "Teacher");
            }

            var awards = _context.Awards.ToList(); // fetch all awards from DB
            return View(awards); // pass to view
        }

        public IActionResult criteria() 
{
            string name = HttpContext.Session.GetString("TeacherName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("TeacherRegister", "Teacher");
            }
            return View();
}
public IActionResult teacherprofile()
{
            string name = HttpContext.Session.GetString("TeacherName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("TeacherRegister", "Teacher");
            }
            return View();
}
public IActionResult editteacherprofile()
{
    return View();
}
public IActionResult viewarts() 
{
            string name = HttpContext.Session.GetString("TeacherName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("TeacherRegister", "Teacher");
            }
            return View();
}
public IActionResult editcriteria() 
{
    return View();
}



        public IActionResult AddAward()
        {
            // Only those names who have uploaded competition art
            var studentNames = _context.C_Arts
                .Where(c => !string.IsNullOrEmpty(c.UploadName)) // safety check
                .Select(c => c.UploadName)
                .Distinct()
                .ToList();

            ViewBag.StudentList = new SelectList(studentNames);

            return View();
        }


        [HttpPost]
        public IActionResult AddAward(Award award)
        {
            // Only allow award for students who have uploaded
            var studentExists = _context.C_Arts
                .Any(c => c.UploadName == award.StudentName);

            if (!studentExists)
            {
                // Rebuild the dropdown before returning
                ViewBag.StudentList = new SelectList(_context.C_Arts
                    .Select(c => c.UploadName).Distinct().ToList());

                ModelState.AddModelError("StudentName", "This student has not uploaded any competition artwork.");
                return View(award);
            }

            if (ModelState.IsValid)
            {
                _context.Awards.Add(award);
                _context.SaveChanges();

                // 👇 Notification set
                HttpContext.Session.SetString("Notification", $"{award.StudentName} won {award.Position} in {award.Title}! 🎉");



                return RedirectToAction("Awards");
            }

            // Rebuild dropdown on error
            ViewBag.StudentList = new SelectList(_context.C_Arts
                .Select(c => c.UploadName).Distinct().ToList());

            return View(award);
        }






        public IActionResult EditAward(int id)
        {
            var award = _context.Awards.FirstOrDefault(a => a.Id == id);
            if (award == null)
            {
                return NotFound();
            }

            return View(award); // pass the model to the view
        }

        [HttpPost]
        public IActionResult EditAward(Award model)
        {
            if (ModelState.IsValid)
            {
                var existingAward = _context.Awards.FirstOrDefault(a => a.Id == model.Id);
                if (existingAward == null)
                {
                    return NotFound();
                }

                existingAward.Title = model.Title;
                existingAward.StudentName = model.StudentName;
                existingAward.Position = model.Position;

                _context.SaveChanges();
                return RedirectToAction("Awards");
            }

            return View(model);
        }



        public IActionResult mystudents()
{
            string name = HttpContext.Session.GetString("TeacherName");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("TeacherRegister", "Teacher");
            }
            return View();
}
// GET: All artworks
public IActionResult arts()
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

        // GET: Remarks form page
        [HttpGet]
        public IActionResult remarks(int id)
        {
            var art = _context.Arts.FirstOrDefault(a => a.Id == id);
            if (art == null)
            {
                return NotFound();
            }

            string uploader = "Unknown";
            if (!string.IsNullOrEmpty(art.UploadedBy))
            {
                var student = _context.Registers.FirstOrDefault(r => r.Email == art.UploadedBy);
                if (student != null)
                {
                    uploader = student.Name;
                }
            }
            else if (!string.IsNullOrEmpty(art.UploaderName))
            {
                uploader = art.UploaderName;
            }

            ViewBag.UploaderName = uploader;
            return View(art);
        }

        // POST: Save remarks
        [HttpPost]
        public IActionResult saveRemarks(int ArtId, string Remarks)
        {
            var art = _context.Arts.FirstOrDefault(a => a.Id == ArtId);
            if (art == null)
            {
                return NotFound();
            }

            var remark = new Remark
            {
                ArtId = ArtId,
                TeacherName = User.Identity?.Name ?? "Teacher",
                Comment = Remarks,
                CreatedAt = DateTime.Now
            };

            _context.Remarks.Add(remark);
            _context.SaveChanges();

            TempData["Message"] = "Remarks submitted successfully!";
            return RedirectToAction("arts");
        }

        public IActionResult CompetitionArt()
        {
            var competitionArts = _context.C_Arts.ToList();
            return View(competitionArts); // Pass list to view
        }

        public IActionResult DeleteAward(int id)
        {
            var award = _context.Awards.FirstOrDefault(a => a.Id == id);
            if (award == null)
            {
                return NotFound();
            }

            _context.Awards.Remove(award);
            _context.SaveChanges();

            return RedirectToAction("Awards");
        }

        public IActionResult TeacherRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TeacherRegister(Teacher model)
        {
            if (ModelState.IsValid)
            {
                _context.Teachers.Add(model);
                await _context.SaveChangesAsync();

                // Safe Set Session
                HttpContext.Session.SetString("Email", model.Email ?? "");
                HttpContext.Session.SetString("Name", model.Name ?? "");
                HttpContext.Session.SetString("Phone", model.Phone ?? "");
                HttpContext.Session.SetString("CreatedDate", model.CreatedDate.ToString());

                return RedirectToAction("TeacherRegister");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> TeacherLogin()
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
            var user = await _context.Teachers
                .FirstOrDefaultAsync(t => t.Email == email && t.Password == password);

            // 4. If found, set session and redirect
            if (user != null)
            {
                HttpContext.Session.SetString("TeacherEmail", user.Email);
                HttpContext.Session.SetString("TeacherName", user.Name ?? "Teacher");
                HttpContext.Session.SetString("TeacherPhone", user.Phone ?? "Teacher");
                HttpContext.Session.SetString("CreatedDate", user.CreatedDate.ToString());

                return RedirectToAction("Home1", "teacher");
            }

            // 5. If invalid credentials
            ViewBag.Error = "Invalid email or password.";
            return View(); // Show error message on login page
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("TeacherRegister");
        }






    }
}

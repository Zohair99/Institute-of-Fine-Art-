using IOFA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace student.Controllers
{
    public class StudentController : Controller
    {
        private readonly YourDbContext _context;
        private readonly IWebHostEnvironment _env;

        public StudentController(YourDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            string name = HttpContext.Session.GetString("Name");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("SpaceRegister", "Institute");
            }
            return View();
        }

        // ✅ GALLERY: Sirf logged-in user ka data
        public IActionResult gallery()
        {
            string userEmail = HttpContext.Session.GetString("Email");

            // 🔹 YAHAN FILTER LAG RAHA HAI: UploadedBy == userEmail
            var artworks = _context.Arts
                .Where(a => a.UploadedBy == userEmail)
                .ToList();

            return View(artworks);
        }

        public IActionResult home()
        {
            string name = HttpContext.Session.GetString("Name");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("Space", "Institute");
            }
            
            
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Space", "Institute");
        }



        public IActionResult profile()
        {
            string name = HttpContext.Session.GetString("Name");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("Space", "Institute");
            }
            return View();
        }

        public IActionResult Details(int id)
        {
            // 🔹 Sirf logged-in student ka artwork uthao
            string userEmail = HttpContext.Session.GetString("Email");
            var art = _context.Arts.FirstOrDefault(a => a.Id == id && a.UploadedBy == userEmail);

            if (art == null)
            {
                return NotFound();
            }

            // Us art ke remarks
            var remarks = _context.Remarks
                .Where(r => r.ArtId == id)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            ViewBag.Remarks = remarks;

            return View(art);
        }

        public IActionResult delete(int id)
        {
            string userEmail = HttpContext.Session.GetString("Email");
            var art = _context.Arts.FirstOrDefault(a => a.Id == id && a.UploadedBy == userEmail);
            if (art == null)
            {
                return NotFound();
            }
            return View(art);
        }

        [HttpPost]
        public IActionResult deleteConfirmed(int id)
        {
            string userEmail = HttpContext.Session.GetString("Email");
            var art = _context.Arts.FirstOrDefault(a => a.Id == id && a.UploadedBy == userEmail);
            if (art == null)
            {
                return NotFound();
            }

            _context.Arts.Remove(art);
            _context.SaveChanges();

            TempData["Message"] = "Artwork deleted successfully.";
            return RedirectToAction("gallery");
        }

        public IActionResult edit(int id)
        {
            string userEmail = HttpContext.Session.GetString("Email");
            var art = _context.Arts.FirstOrDefault(a => a.Id == id && a.UploadedBy == userEmail);
            if (art == null)
            {
                return NotFound();
            }
            return View(art);
        }

        [HttpPost]
        public async Task<IActionResult> edit(int id, string Title, string Description, IFormFile? ArtworkImage)
        {
            string userEmail = HttpContext.Session.GetString("Email");
            var art = _context.Arts.FirstOrDefault(a => a.Id == id && a.UploadedBy == userEmail);
            if (art == null)
            {
                return NotFound();
            }

            art.Title = Title;
            art.Description = Description;

            if (ArtworkImage != null && ArtworkImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "UploadedFiles");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = Path.GetFileName(ArtworkImage.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ArtworkImage.CopyToAsync(stream);
                }

                art.Path = "/UploadedFiles/" + fileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("gallery");
        }

        public IActionResult upload()
        {
            string name = HttpContext.Session.GetString("Name");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("Space", "Institute");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(Art model)
        {
            var file = Request.Form.Files.FirstOrDefault();

            if (file != null && file.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "UploadedFiles");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var email = HttpContext.Session.GetString("Email");
                string uploaderName = Request.Form["UploaderName"];

                // 🔥 Try getting name from session email (from Register table)
                if (!string.IsNullOrEmpty(email))
                {
                    var student = await _context.Registers.FirstOrDefaultAsync(r => r.Email == email);
                    if (student != null)
                    {
                        uploaderName = student.Name; // 👈 Overwrite UploaderName with logged-in student's name
                    }
                }

                var art = new Art
                {
                    Title = Request.Form["Title"],
                    Description = Request.Form["Description"],
                    Path = "/UploadedFiles/" + fileName,
                    CreatedAt = DateTime.Now,
                    UploadedBy = email, // can be null if not logged in
                    UploaderName = uploaderName
                };

                _context.Arts.Add(art);
                await _context.SaveChangesAsync();

                return RedirectToAction("gallery", "student"); // Or wherever you want to redirect
            }
            else
            {
                ViewBag.Message = "Please select a file.";
                return View();
            }
        }


        public IActionResult editprofile()
        {
            return View();
        }

        public IActionResult awards()
        {
            string name = HttpContext.Session.GetString("Name");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("Space", "Institute");
            }
            var studentName = HttpContext.Session.GetString("Name");

            List<Award> awards;

            if (!string.IsNullOrEmpty(studentName))
            {
                awards = _context.Awards
                    .Where(a => a.StudentName.ToLower() == studentName.ToLower())
                    .ToList();
            }
            else
            {
                awards = new List<Award>(); // Session missing → empty list
            }

            return View(awards);
        }





        public IActionResult Competition()
        {
            string name = HttpContext.Session.GetString("Name");

            // Check session: agar login nahi kiya to login page pe bhej do
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("Space", "Institute");
            }
            var competitions = _context.Compititions.ToList();  // fetch all competitions added by teacher
            return View(competitions);  // pass to view
        }

        public IActionResult CompetitionUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompetitionUpload(string Title, string Description, IFormFile ArtworkImage)
        {
            string uniqueFileName = null;

            if (ArtworkImage != null && ArtworkImage.Length > 0)
            {
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadsPath);

                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ArtworkImage.FileName);
                var filePath = Path.Combine(uploadsPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ArtworkImage.CopyToAsync(stream);
                }
            }

            // 🔴 Get uploader info from session
            string uploaderName = HttpContext.Session.GetString("Name");
            string uploaderEmail = HttpContext.Session.GetString("Email");

            var art = new C_Art
            {
                Title = Title,
                Description = Description,
                UploadName = uploaderName,
                UploadEmal = uploaderEmail,
                ImagePath = uniqueFileName
            };

            _context.C_Arts.Add(art);
            await _context.SaveChangesAsync();

            return RedirectToAction("competition"); // Redirect after successful upload
        }

        [HttpPost]
        public IActionResult ClearNotification()
        {
            HttpContext.Session.Remove("Notification");
            return Ok();
        }



public IActionResult CompetitionArt()
    {
        var userEmail = HttpContext.Session.GetString("Email");

        if (string.IsNullOrEmpty(userEmail))
        {
            // Redirect to login or show error
            return RedirectToAction("Login", "Account");
        }

        var competitionArts = _context.C_Arts
            .Where(c => c.UploadEmal.ToLower() == userEmail.ToLower())
            .ToList();

        return View(competitionArts);
    }

        [HttpPost]
        public IActionResult DeleteArt(int id)
        {
            var art = _context.C_Arts.FirstOrDefault(a => a.Id == id);

            if (art == null)
                return NotFound();

            // Optional: Add check to allow delete only by uploader
            var userEmail = HttpContext.Session.GetString("Email");
            if (art.UploadEmal.ToLower() != userEmail.ToLower())
                return Unauthorized();

            _context.C_Arts.Remove(art);
            _context.SaveChanges();

            return RedirectToAction("CompetitionArt");
        }











    }
}

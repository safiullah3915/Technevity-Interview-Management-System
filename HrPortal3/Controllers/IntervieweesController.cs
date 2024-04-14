using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HrPortal3.Models;
using HrPortal3.Data;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Identity;

namespace HrPortal3.Controllers
{
    public class IntervieweesController : Controller
    {
        private readonly ApplicationDbContext _context;
       private readonly IWebHostEnvironment hostingEnvionment;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public IntervieweesController(ApplicationDbContext context,
             IWebHostEnvironment hostingEnvionment, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            this.hostingEnvionment = hostingEnvionment;
        }

        public async Task<IActionResult> Index()
        {
            var interviewees = await _context.Interviewee.ToListAsync();

            // Fetch Panel names
            var panelIds = interviewees.Select(i => i.PanelId).Distinct();
            var panels = await _context.Panel.Where(p => panelIds.Contains(p.PanelId)).ToListAsync();

            // Fetch Post names
            var postIds = interviewees.Select(i => i.PostId).Distinct();
            var posts = await _context.Post.Where(p => postIds.Contains(p.PostId)).ToListAsync();

            var intervieweeList = new List<IntervieweeIndexModel>();

            foreach (var i in interviewees)
            {
                // Fetch User names for the current interviewee
                var userIds = i.UserId?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var users = await _context.Users.Where(u => userIds.Contains(u.Id)).Select(u => u.Name).ToListAsync();

                intervieweeList.Add(new IntervieweeIndexModel
                {
                    IntervieweeId = i.IntervieweeId,
                    Name = i.Name,
                    Contact = i.Contact,
                    ResumeFile = i.ResumeFile,
                    InterviewDate = i.InterviewDate,
                    PanelId = i.PanelId,
                    PostId = i.PostId,
                    PanelName = panels.FirstOrDefault(p => p.PanelId == i.PanelId)?.PanelName,
                    PostName = posts.FirstOrDefault(p => p.PostId == i.PostId)?.PostName,
                    UserName = string.Join(", ", users) // Join user names into a single string
                });
            }

            return View(intervieweeList);
        }



        // GET: Interviewees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Interviewee == null)
            {
                return NotFound();
            }

            var interviewee = await _context.Interviewee
                .FirstOrDefaultAsync(m => m.IntervieweeId == id);
            if (interviewee == null)
            {
                return NotFound();
            }

            return View(interviewee);
        }

        // GET: Interviewees/Create
        public IActionResult Create()
        {
            var panels = _context.Panel.ToList();
            ViewBag.Panels = new SelectList(panels, "PanelId", "PanelName");
                      

            var interviewers = _userManager.GetUsersInRoleAsync("Interviewer").Result;
            ViewBag.Users = new SelectList(interviewers, "Id", "Name");

            var posts = _context.Post.ToList();
            ViewBag.Posts = new SelectList(posts, "PostId", "PostName");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IntervieweeId,Name,Contact,ResumeFile,Resume,InterviewDate,PanelId,PostId,UserId")] IntervieweeCreateModel model)
        {
            if (ModelState.IsValid)
            {
                // Convert the selected UserIds to a list
                List<string> selectedUserIds = model.UserId ?? new List<string>();

                string uniqueFileName = ProcessUploadedFile(model);
                Interviewee newInterviewee = new Interviewee
                {
                    Name = model.Name,
                    Contact = model.Contact,
                    InterviewDate = model.InterviewDate,
                    PanelId = model.PanelId,
                    PostId = model.PostId,
                    ResumeFile = uniqueFileName,
                    UserId = string.Join(",", selectedUserIds) // Convert the list to a comma-separated string
                };
                _context.Add(newInterviewee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        // GET: Interviewees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Interviewee == null)
            {
                return NotFound();
            }

            var panels = _context.Panel.ToList();
            ViewBag.Panels = new SelectList(panels, "PanelId", "PanelName");

            var interviewers = _userManager.GetUsersInRoleAsync("Interviewer").Result;
            ViewBag.Users = new SelectList(interviewers, "Id", "Name");

            var posts = _context.Post.ToList();
            ViewBag.Posts = new SelectList(posts, "PostId", "PostName");

            var interviewee = await _context.Interviewee.FindAsync(id);
            IntervieweeEditModel interviewEditModel = new IntervieweeEditModel
            {
                IntervieweeId = interviewee.IntervieweeId,
                Name = interviewee.Name,
                Contact = interviewee.Contact,
                InterviewDate = interviewee.InterviewDate,
                PostId = interviewee.PostId,
                PanelId = interviewee.PanelId,
                ExistingResumePath = interviewee.ResumeFile,
                // Convert the UserId string to a list of strings
                UserId = interviewee.UserId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()
            };

            if (interviewee == null)
            {
                return NotFound();
            }
            return View(interviewEditModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IntervieweeId,Name,Contact,ResumeFile,Resume,ExistingResumePath,InterviewDate,PanelId,UserId,PostId")] IntervieweeEditModel model)
        {
            var interviewee = await _context.Interviewee.FindAsync(model.IntervieweeId);
            if (ModelState.IsValid)
            {
                if (interviewee == null)
                {
                    return NotFound(); // Interviewee with the specified ID not found, handle the error.
                }

                interviewee.Name = model.Name;
                interviewee.Contact = model.Contact;
                interviewee.InterviewDate = model.InterviewDate;
                interviewee.PanelId = model.PanelId;
                interviewee.PostId = model.PostId;
                // Convert the list of strings to a comma-separated string for UserId
                interviewee.UserId = string.Join(",", model.UserId);

                if (model.Resume != null)
                {
                    // Update the ResumeFile property with the new file
                    interviewee.ResumeFile = ProcessUploadedFile(model);
                }

                _context.Update(interviewee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }


        private string ProcessUploadedFile(IntervieweeCreateModel model)
        {
            string uniqueFileName = null;
            if (model.Resume != null)
            {
                string uploadFolder = Path.Combine(hostingEnvionment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Resume.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                model.Resume.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            return uniqueFileName;
        }
        // GET: Interviewees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Interviewee == null)
            {
                return NotFound();
            }

            var interviewee = await _context.Interviewee
                .FirstOrDefaultAsync(m => m.IntervieweeId == id);
            if (interviewee == null)
            {
                return NotFound();
            }

            return View(interviewee);
        }

        // POST: Interviewees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Interviewee == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Interviewee'  is null.");
            }
            var interviewee = await _context.Interviewee.FindAsync(id);
            if (interviewee != null)
            {
                _context.Interviewee.Remove(interviewee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DownloadFile(string filePath)
        {
           var path = Path.Combine(hostingEnvionment.WebRootPath, "images", filePath);

                   var memory = new MemoryStream();
                    using (var stream = new FileStream(path, FileMode.Open))
                   {
                       await stream.CopyToAsync(memory);
                   }
               memory.Position = 0;
                    var contentType = "APPLICATION/octet-stream";
                   var fileName = Path.GetFileName(path);

                    return File(memory, contentType, fileName);
               }

        private bool IntervieweeExists(int id)
        {
            return (_context.Interviewee?.Any(e => e.IntervieweeId == id)).GetValueOrDefault();
        }
    }
}

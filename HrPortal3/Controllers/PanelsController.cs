using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HrPortal3.Models;
using HrPortal3.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace HrPortal3.Controllers
{
    public class PanelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public PanelsController(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
           _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Panels
        public async Task<IActionResult> Index()
        {
              return _context.Panel != null ? 
                          View(await _context.Panel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Panel'  is null.");
        }

        // GET: Panels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Panel == null)
            {
                return NotFound();
            }

            var panel = await _context.Panel
                .FirstOrDefaultAsync(m => m.PanelId == id);
            if (panel == null)
            {
                return NotFound();
            }

            return View(panel);
        }

        // GET: Panels/Create
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_roleManager.Roles.Select(r => r.Name));
            return View();
        }

        // POST: Panels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("PanelId,PanelName")] Panel panel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(panel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(panel);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PanelId,PanelName,PanelMemberEmail,PanelMemberPassword,PanelMemberRole")] Panel panel)
        {
            if (ModelState.IsValid)
            {
                // Save the panel data to the database
                _context.Add(panel);
                await _context.SaveChangesAsync();

                // Create Panel Member with Role and Credentials
             

                return RedirectToAction(nameof(Index));
            }

            // Prepare the roles for the dropdown in the view
         

            return View(panel);
        }

        // GET: Panels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Panel == null)
            {
                return NotFound();
            }

            var panel = await _context.Panel.FindAsync(id);
            if (panel == null)
            {
                return NotFound();
            }
            return View(panel);
        }

        // POST: Panels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PanelId,PanelName")] Panel panel)
        {
            if (id != panel.PanelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(panel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PanelExists(panel.PanelId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(panel);
        }

        // GET: Panels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Panel == null)
            {
                return NotFound();
            }

            var panel = await _context.Panel
                .FirstOrDefaultAsync(m => m.PanelId == id);
            if (panel == null)
            {
                return NotFound();
            }

            return View(panel);
        }

        // POST: Panels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Panel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Panel'  is null.");
            }
            var panel = await _context.Panel.FindAsync(id);
            if (panel != null)
            {
                _context.Panel.Remove(panel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PanelExists(int id)
        {
          return (_context.Panel?.Any(e => e.PanelId == id)).GetValueOrDefault();
        }
    }
}

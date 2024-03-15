using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tecnica.Data;
using Tecnica.Models;

namespace Tecnica.Controllers
{
    public class RoleModels1Controller : Controller
    {
        private readonly TecnicaContext _context;

        public RoleModels1Controller(TecnicaContext context)
        {
            _context = context;
        }

        // GET: RoleModels1
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoleModel.ToListAsync());
        }

        // GET: RoleModels1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleModel = await _context.RoleModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleModel == null)
            {
                return NotFound();
            }

            return View(roleModel);
        }

        // GET: RoleModels1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoleModels1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,PermisosId,Permisos")] RoleModel roleModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roleModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roleModel);
        }

        // GET: RoleModels1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleModel = await _context.RoleModel.FindAsync(id);
            if (roleModel == null)
            {
                return NotFound();
            }
            return View(roleModel);
        }

        // POST: RoleModels1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,PermisosId,Permisos")] RoleModel roleModel)
        {
            if (id != roleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roleModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleModelExists(roleModel.Id))
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
            return View(roleModel);
        }

        // GET: RoleModels1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleModel = await _context.RoleModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleModel == null)
            {
                return NotFound();
            }

            return View(roleModel);
        }

        // POST: RoleModels1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roleModel = await _context.RoleModel.FindAsync(id);
            if (roleModel != null)
            {
                _context.RoleModel.Remove(roleModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleModelExists(int id)
        {
            return _context.RoleModel.Any(e => e.Id == id);
        }
    }
}

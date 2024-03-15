using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnica.Data;
using Tecnica.Models;

namespace Tecnica.Controllers
{
    public class PermisoModelsController : Controller
    {
        private readonly TecnicaContext _context;

        public PermisoModelsController(TecnicaContext context)
        {
            _context = new TecnicaContext(new DbContextOptionsBuilder<TecnicaContext>()
            .UseInMemoryDatabase("TecnicaDatabase")
            .Options);
        }

        // GET: PermisoModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PermisoModel.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: PermisoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] PermisoModel permisoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permisoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permisoModel);
        }

        private bool PermisoModelExists(int id)
        {
            return _context.PermisoModel.Any(e => e.Id == id);
        }
    }
}

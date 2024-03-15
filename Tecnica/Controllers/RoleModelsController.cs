using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tecnica.Data;
using Tecnica.Models;

namespace Tecnica.Controllers
{
    public class RoleModelsController : Controller
    {
        private readonly TecnicaContext _context;

        public RoleModelsController(TecnicaContext context)
        {
            _context = new TecnicaContext(new DbContextOptionsBuilder<TecnicaContext>()
            .UseInMemoryDatabase("TecnicaDatabase")
            .Options);
        }

        // Obtener los Roles de la base de datos
        public async Task<IActionResult> Index()
        {
            var roles = await _context.RoleModel.ToListAsync();
            //añadir una cadena de permisos a cada rol segun los permisos que tenga dond el id del permiso este en la cadena idPermisos separados por comas
            foreach (var r in roles)
            {
                if (r.PermisosId != null)
                {
                    string[] permisos = r.PermisosId.Split(',');
                    string permisosName = "";
                    foreach (var p in permisos)
                    {
                        var permiso = await _context.PermisoModel.FindAsync(int.Parse(p));
                        if (permiso != null)
                        {
                            if (permisosName == "")
                            {
                                permisosName = permiso.Nombre;
                            }
                            else
                            {
                                permisosName = permisosName + "," + permiso.Nombre;
                            }
                        }
                    }
                    r.Permisos = permisosName;

                }
            }
            return View(roles);
        }
        // GET: RoleModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoleModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,PermisosId")] RoleModel roleModel)
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


        //Agregar permisos a rol
        public async Task<IActionResult> AddPermiso(int IdRol, int IdPermiso)
        {
            var rol = await _context.RoleModel.FindAsync(IdRol);
            var permiso = await _context.PermisoModel.FindAsync(IdPermiso);
            if (rol != null && permiso != null)
            {
                if (rol.PermisosId == null)
                {
                    rol.PermisosId = permiso.Id.ToString();
                }
                else
                {
                    rol.PermisosId = rol.PermisosId + "," + permiso.Id.ToString();
                }
                _context.Update(rol);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));

        }

        //Quitar permisos a rol
        public async Task<IActionResult> LessPermiso(int IdRol, int IdPermiso)
        {
            var rol = await _context.RoleModel.FindAsync(IdRol);
            var permiso = await _context.PermisoModel.FindAsync(IdPermiso);
            if (rol != null && permiso != null)
            {
                if (rol.PermisosId != null)
                {
                    string[] permisos = rol.PermisosId.Split(',');
                    string newPermisos = "";
                    foreach (var p in permisos)
                    {
                        if (p != IdPermiso.ToString())
                        {
                            if (newPermisos == "")
                            {
                                newPermisos = p;
                            }
                            else
                            {
                                newPermisos = newPermisos + "," + p;
                            }
                        }
                    }
                    rol.PermisosId = newPermisos;
                    _context.Update(rol);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var RoleModel = await _context.RoleModel.FindAsync(id);
            if (RoleModel == null)
            {
                return NotFound();
            }
            return View(RoleModel);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id)
        {
            var roleModel = await _context.RoleModel.FindAsync(id);
            var permisoModel = await _context.PermisoModel.ToListAsync();
            if (roleModel != null)
            {
                string[] permisos = roleModel.PermisosId.Split(',');
                string[] permisosName;
                foreach (var p in permisos)
                {
                    //return View(roleModel);
                }
                return View(roleModel);


            }
            return RedirectToAction(nameof(Index));
        }

        private bool RoleModelExists(int id)
        {
            return _context.RoleModel.Any(e => e.Id == id);
        }
    }
}

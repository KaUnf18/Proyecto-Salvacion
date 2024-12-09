using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Salvacion.Data;
using Proyecto_Salvacion.Models;

namespace Proyecto_Salvacion.Controllers
{
    public class RolesController : Controller
    {
        private readonly SalonComunalContext _context;

        public RolesController(SalonComunalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _context.Roles.ToListAsync();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rol rol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null) return NotFound();
            return View(rol);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Rol rol)
        {
            if (id != rol.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(rol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null) return NotFound();
            return View(rol);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

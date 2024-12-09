using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Salvacion.Data;
using Proyecto_Salvacion.Models;

namespace Proyecto_Salvacion.Controllers
{
    public class ReservasController : Controller
    {
        private readonly SalonComunalContext _context;

        public ReservasController(SalonComunalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservas.Include(r => r.ReservaProductos).ThenInclude(rp => rp.Producto).ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Productos"] = new SelectList(_context.Productos, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reserva reserva, List<int> productoIds, List<int> cantidades)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();

                // Agregar productos a la reserva
                for (int i = 0; i < productoIds.Count; i++)
                {
                    _context.ReservaProductos.Add(new ReservaProducto
                    {
                        ReservaId = reserva.Id,
                        ProductoId = productoIds[i],
                        Cantidad = cantidades[i]
                    });
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(reserva);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var reserva = await _context.Reservas.Include(r => r.ReservaProductos).FirstOrDefaultAsync(r => r.Id == id);
            if (reserva == null) return NotFound();
            ViewData["Productos"] = new SelectList(_context.Productos, "Id", "Nombre");
            return View(reserva);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Reserva reserva)
        {
            if (id != reserva.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reserva);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return NotFound();
            return View(reserva);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CompletePayment(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return NotFound();

            reserva.Confirmado = true;
            await _context.SaveChangesAsync();

            // Lógica para enviar un correo de confirmación (se puede agregar aquí o en un servicio separado)

            return RedirectToAction(nameof(Index));
        }
    }
}

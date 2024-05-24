using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Controllers
{
    public class PacientesController : Controller
    {
        private readonly DbContextLab _context = null;

        public PacientesController(DbContextLab context)
        {
            _context = context;
        }

        // -------------------------- LISTA PACIENTES --------------------------

        public async Task<IActionResult> Index()
        {
            var listaPacientes = await _context.Pacientes.ToListAsync();
            return View(listaPacientes);
        }

        // -------------------------- CREAR PACIENTES --------------------------

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paciente paciente)
        {
            if (paciente == null)
            {

                return View();
            }
            else
            {
                var cita = await _context.Pacientes.FirstOrDefaultAsync(r => r.Fecha == paciente.Fecha);
                if (cita != null)
                {
                    ModelState.AddModelError("Fecha", "Ya hay una cita para esta fecha");
                    return View();
                }

                _context.Pacientes.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

        }//Fin Create

    }//Fin PacientesController
}//Fin namespace Lab1.Controllers   

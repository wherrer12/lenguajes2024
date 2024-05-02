using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Tarea_3_Clinica_Dental.Models;

namespace Tarea_4_Clinica_Dental.Controllers
{
    public class PacientesController : Controller
    {
        private readonly DbContextClinicaDental _context = null;

        public PacientesController(DbContextClinicaDental context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var listado = await _context.Pacientes.ToListAsync();

            return View(listado);
        }

        // -------------------------- CREAR ------------------------------------

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] Paciente paciente)
        {
            if (paciente == null)//Validacion de datos
            {
                return View();
            }
            else
            {
                //Validacion de cita
                var cita = await _context.Pacientes.FirstOrDefaultAsync(r => r.Fecha == paciente.Fecha);
                if (cita != null)
                {
                    ModelState.AddModelError("Fecha", "Ya existe una cita para esta fecha");
                    return View();
                }

                //
                _context.Pacientes.Add(paciente);//Agrega el paciente al contexto
                await _context.SaveChangesAsync();//Agrega datos a la base de datos
                EnviarEmail(paciente);//Envia correo
                return RedirectToAction("Index");

            }

        }

        // -------------------------- BORRAR ------------------------------------

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var temp = await _context.Pacientes.FirstOrDefaultAsync(r => r.idPaciente == id);

            return View(temp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int? id)
        {

            var temp = await _context.Pacientes.FirstOrDefaultAsync(r => r.idPaciente == id);


            if (temp != null)
            {
                _context.Pacientes.Remove(temp);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            else
            {
                return NotFound();
            }

        }

        // -------------------------- EDITAR ------------------------------------

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var temp = await _context.Pacientes.FirstOrDefaultAsync(r => r.idPaciente == id);

            return View(temp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind] Paciente fPaciente)
        {

            if (id == fPaciente.idPaciente)
            {

                var temp = await _context.Pacientes.FirstOrDefaultAsync(r => r.idPaciente == id);

                _context.Pacientes.Remove(temp);
                _context.Pacientes.Add(fPaciente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {

                return NotFound();

            }
        }

        // -------------------------- ENVIAR CORREO ------------------------------------
        //Agregar metodo para enviar correo
        public bool EnviarEmail(Paciente paciente)
        {
            try
            {
                bool enviar = true;
                Email email = new Email();
                email.Enviar(paciente);
                enviar = true;
                return enviar;

            }
            catch (Exception ex)
            {
               return false;

            }
         
        }
        
    }//Fin clase

}//Fin namespace

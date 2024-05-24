//using Microsoft.AspNetCore.Mvc;
//using Examen_1_B43383.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Authentication;
//using System.Security.Claims;

//namespace Examen_1_B43383.Controllers
//{
//    public class DiagnosticoController : Controller
//    {
//        private readonly DbContextCita _context = null;

//        public DiagnosticoController(DbContextCita context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public async Task<IActionResult> Edit(int id)
//        {
//            var temp = await _context.Diagnosticos.FirstOrDefaultAsync(r => r.IdDiag == id);
//            return View(temp);
//        }


//        [HttpPost]
//        public async Task<IActionResult> Edit(int id, [Bind] Cita citas, Diagnostico resultado)
//        {
//            if (id == citas.Id)
//            {            
//                _context.Diagnosticos.Add(resultado);
//                await _context.SaveChangesAsync();
//                Email email = new Email();
//                email.EnviarObservacion(citas, resultado);
//                return RedirectToAction("Index", "Cita");

//            }
//            else
//            {
//                return NotFound();

//            }
//        }
//    }
//}

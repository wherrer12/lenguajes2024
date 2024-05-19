using Microsoft.AspNetCore.Mvc;
using Examen_1_B43383.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Examen_1_B43383.Controllers
{
    public class CitaController : Controller
    {
        private readonly DbContextCita _context = null;

        public CitaController(DbContextCita context){

            _context = context;
        }

        public async Task<IActionResult> Index()
        {  
            var listado = await _context.citas.ToListAsync();

            return View(listado); 
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind] Cita cita)
        {
            var temp = ValidarUsuario(cita);

            if (temp != null)
            {
                bool restablecer = false;

                if (restablecer)
                {
                    return RedirectToAction("Restablecer", "Usuarios", new { Email = cita.Email });
                }
                else
                {
                    var userClaims = new List<Claim>() { new Claim(ClaimTypes.Name, cita.Email) };
                    var granIdentity = new ClaimsIdentity(userClaims, "User Identity");
                    var userPrincipal = new ClaimsPrincipal(new[] { granIdentity });
                    await HttpContext.SignInAsync(userPrincipal);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Mensaje"] = "Error el usuario o contraseña no son correctos...";

                return View(cita);
            }

        }//Fin metodo ValidarUsuario

        private Cita ValidarUsuario(Cita temp)
        {
            Cita autorizado = null;
            var user = _context.citas.FirstOrDefault(u => u.Email == temp.Email);

            if (user != null)
            {
                if (user.Password.Equals(temp.Password))
                {
                    autorizado = user;
                }
            }

            return autorizado;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] Cita citas)
        { 
            if (citas == null)
            {
                return NotFound(); 
            }
            else 
            {
                citas.FechaHoraRevision = DateTime.Now; 
                citas.Estado = 'P'; 

                _context.citas.Add(citas);

                try 
                {
   
                    await _context.SaveChangesAsync();
                    Email email = new Email();
                    email.Enviar(citas);
                    return RedirectToAction("Login", "Cita");
                }
                catch (Exception ex)
                {

                    TempData["Mensaje"] = "No se creo la cuenta..";
                    return View(); 
                }

            }
        }

    }//Fin clase

}//Fin namespace

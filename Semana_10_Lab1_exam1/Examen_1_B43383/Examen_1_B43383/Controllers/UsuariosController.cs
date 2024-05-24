using Microsoft.AspNetCore.Mvc;
using Examen_1_B43383.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Examen_1_B43383.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly DbContextCita _context = null;

        public UsuariosController(DbContextCita uContext)
        {
            _context = uContext;
        }

        public async Task<IActionResult> Index()
        {
            var listado = await _context.Citas.ToListAsync();

            return View(listado);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind] Usuario admin)
        {
            var temp = ValidarUsuario(admin);

            if (temp != null)
            {
                var userClaims = new List<Claim>() { new Claim(ClaimTypes.Name, admin.Email) };
                var granIdentity = new ClaimsIdentity(userClaims, "User Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { granIdentity });
                await HttpContext.SignInAsync(userPrincipal);
                return RedirectToAction("Index", "Cita");

            }
            else
            {
                TempData["Mensaje"] = "Error el usuario o contraseña no son correctos...";

                return View(temp);
            }

        }//Fin login

        private Usuario ValidarUsuario(Usuario temp)
        {
            Usuario autorizado = null;
            var user = _context.Usuarios.FirstOrDefault(u => u.Email == temp.Email);

            if (user != null)
            {
                if (user.Password.Equals(temp.Password))
                {
                    autorizado = user;
                }
            }

            return autorizado;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }//Fin de la clase
}//Fin del namespace

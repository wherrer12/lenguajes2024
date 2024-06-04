using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;
using WebHotelBeach.Models;

namespace WebHotelBeach.Controllers
{
    public class UsuarioController : Controller
    {
        //db context de la base
        private readonly DbContextHotelesBeach _context = null;

 
        public UsuarioController(DbContextHotelesBeach context)
        {
            _context = context;
        }


        //---------------------------------------------------------------------------
        //GET del login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        //---------------------------------------------------------------------------
        //POST del login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind] Usuario temp)
        {
            //se busca en la lista que existe el usuario digitado
            var user = _context.Usuario.FirstOrDefault(u => u.Email.Equals(temp.Email));

            
            //verifica si el usuario existo o si esta digitando bien la contra
            if (user == null || !user.Password.Equals(temp.Password))
            {
                //datos incorrectos
                TempData["Mensaje"] = "Usuario o Clave Incorrectos";
                return View();
            }
            else
            {
                //de ser correcto, tambien hacemos un recorrido por los clientes
                //buscamos que cliente tiene el email
                var cliente = _context.Cliente.FirstOrDefault(c => c.Email.Equals(user.Email));

                //si se ecuentra ese cliente
                if (cliente != null)
                {
                    //buscamos cual es su rol
                    var rolSistema = cliente.RolSistema;
                    //buscamos cual es su id
                    var idSistema = cliente.Id;
                    //se guarda la informacion necesario para la seguridad de la pagina
                    var userClaims = new List<Claim>
                        {
                             new Claim(ClaimTypes.Name, user.Email),
                             new Claim("NombreUsuario", user.NombreCompleto),
                             new Claim("Rol", rolSistema),
                             new Claim("id", idSistema.ToString())
                        };

                    //se crea el tipo de entidad
                    var grandIdemtity = new ClaimsIdentity(userClaims, "userIdentity");

                    //se instancia la entidad principal
                    var userPrincipal = new ClaimsPrincipal(new[] { grandIdemtity });

                    //se incia sesion
                    await HttpContext.SignInAsync(userPrincipal);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //no existe un cliente con ese email
                    TempData["Mensaje"] = "Usuario no asociado a un cliente";
                    return View();
                }


            }
        }



        //-------------------------------------------------------------------------
        //Metodo de cerrar ssion
        public async Task<IActionResult> Logout()
        {
            //se cierra sesion
            await HttpContext.SignOutAsync();

            //lo envia al menu principal
            return RedirectToAction("Index", "Home");
        }

        

    }//cierre class
}//cierre namespace

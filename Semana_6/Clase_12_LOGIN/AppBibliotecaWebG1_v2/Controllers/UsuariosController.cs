using Microsoft.AspNetCore.Mvc;
using AppBibliotecaWebG1.Models;

namespace AppBibliotecaWebG1.Controllers
{
    public class UsuariosController : Controller
    {
        //Variable para manejar la referencia del ORM
        private readonly DbContextBiblioteca _context = null;

        //Constructor con parametros

        public UsuariosController(DbContextBiblioteca pContext)
        {
            _context = pContext; //Se asigna la referencia del ORM
        }

        /// <summary>
        /// Metodo encargado de mostrar el formulario de autenticacion
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        /// <summary>
        /// Metodo encargado de mostrar el front end para crear una cuenta a un usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CrearCuenta()
        {
            return View();
        }

        //Metodo encargado de crear una cuenta de usuario
        public async Task<IActionResult> CrearCuenta([Bind]Usuario pUser) {

            //Se valida si tiene datos
            if (pUser == null)
            {
                return NotFound(); //Error 404, recurso no disponible
            }
            else { 
            
                //Se valida si tiene datos
                pUser.FechaRegistro = DateTime.Now; //Se asigna la fecha registro
                pUser.Estado = 'A'; //Se indica que su estado activo
                pUser.Restablecer = 'S'; //Se indica que se debe restablecer la clave

                //Se genera la clave temporal
                pUser.Password = GenerarClave();

                //Se agrega el usuario al catalogo
                _context.Usuarios.Add(pUser);

                try //Se intenta aplicar cambios y enviar el email
                {
                    //Se aplican los cambios
                    await _context.SaveChangesAsync();

                    //Se crea una instancia del objeto email
                    Email email = new Email();

                    //Se envia el como parametros los datos del usuario que recibe la clave temporal
                    email.Enviar(pUser);

                    //Si todo esta bien se logro enviar el correo
                    //Se ubica al usuario dentro del formulario de login
                    return RedirectToAction("Login", "Usuarios");

                }
                catch (Exception ex)//Ojo la variable ex almacena la informacion del error
                {
                    TempData["Mensaje"] = "Error al crear la cuenta.." +
                        "Verifique el siguiente mensaje de error: " + ex.Message;

                    return View(); //Se ubica al usuario dentro del formulario de crear cuenta
                }
            
            }

        }

        //Metodo encargado de generar un valor aleatorio
        public string GenerarClave()
        {
            //Variable para generar un valor aleatorio
            Random rnd = new Random();

            //Variable para almacenar la clave
            string clave = string.Empty;

            //Caracteres que se pueden utilizar para generar la clave temporal
            clave = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            //Se genera la clave aleatoria
            return new string(Enumerable.Repeat(clave, 12).Select(
                s => s[rnd.Next(s.Length)]).ToArray());

        }

    }
}

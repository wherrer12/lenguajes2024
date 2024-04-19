using Microsoft.AspNetCore.Mvc;

namespace AppBibliotecaWebG1.Controllers
{
    public class UsuariosController : Controller
    {

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

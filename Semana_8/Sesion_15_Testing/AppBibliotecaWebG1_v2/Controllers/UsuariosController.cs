using Microsoft.AspNetCore.Mvc;
using AppBibliotecaWebG1.Models;
using Microsoft.EntityFrameworkCore;

//Librerias necesarias para el proceso de autenticacion
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace AppBibliotecaWebG1.Controllers
{
    public class UsuariosController : Controller
    {
        //Variable para manejar la referencia del ORM
        private readonly DbContextBiblioteca _context = null;

        //Variable para controlar el email a restablecer
        private static string EmailRestablecer = "";



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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind] Usuario user) { 
            
            //Se utiliza el metodo para validar los datos de el usuario
            var temp = ValidarUsuario(user);

            //Se verifica si hay datos
            if (temp != null) { 
                
                bool restablecer = VerificarRestablecer(temp);//Variable control

                //Se verifica si necesita restablecer
                restablecer = VerificarRestablecer(temp);

                if(restablecer)
                {
                    //Se ubica al usuario dentro de formulario para restablecer
                    return RedirectToAction("Restablecer","Usuarios", new {Email = temp.Email});
                }
                else//Ya realizo anterioirmente el proceso de restablecer
                {
                    //Se procesa la autenticacion
                    //Se crea la instancia 
                    var userClaims = new List<Claim>() { new Claim(ClaimTypes.Name, temp.Email)};

                    //Se crea el tipo de la identidad
                    var grandIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    //Se instancia la entidad principal
                    var userPrincipal = new ClaimsPrincipal(new[] { grandIdentity });

                    //Se realiza la autenticacion dentro del contexto Http se envia la entidad como parametro
                    await HttpContext.SignInAsync(userPrincipal);

                    //Se ubica al usuario dentro de la pagina de inicio
                    return RedirectToAction("Index", "Home");

                }
            
            }
            else //Si no hay datos
            {
                TempData["Mensaje"] = "Error, el usuario o contraseña es incorrecto";
                return View(temp);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Restablecer(string? Email)
        {
            //Se toman los datos del usuario que debe restablecer la contraseña
            var temp = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);

            //Se instancia el objeto restablecer
            SeguridadRestablecer restablecer = new SeguridadRestablecer();

            //Se rellena los datos del usuario a restablecer
            restablecer.Email = temp.Email;
            restablecer.Password = temp.Password;

            //Se almacena el email del usuario a restablecer
            EmailRestablecer = temp.Email;

            //se envian los datos del usuario a restablecer el password
            return View(restablecer);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restablecer([Bind] SeguridadRestablecer pRestablecer)
        {

            if (pRestablecer != null)//Se valida si hay datos
            {
                //Se identifica al usuario que vamos a restablecer la contraseña
                var temp = await _context.Usuarios.FirstOrDefaultAsync(
                    u => u.Email.Equals(EmailRestablecer));

                if (temp != null)
                {
                    //Se realiza el proceso de verficacion del password
                    if (temp.Password.Equals(pRestablecer.Password))
                    {
                        //Se realiza el proceso de restablecer
                        //Asignacion de la nueva clave
                        temp.Password = pRestablecer.Confirmar;

                        //Se indica que ya se realizo el proceso de restablecer
                        temp.Restablecer = 'N';

                        //Se actualiza los datos
                        _context.Usuarios.Update(temp);

                        //Se aplican los cambios
                        await _context.SaveChangesAsync();

                        //Se ubica al usuario dentro del proceso de login
                        return RedirectToAction("Login", "Usuarios");

                    }
                    else {
                        //Se muestra el siguiente mensaje error 
                        TempData["Mensaje"] = "El password es incorrecto";

                        //Se ubica al usuario dentro del formulario de restablecer
                        return View(pRestablecer);
                    }

                }
                else {
                    //Se muestra el siguiente mensaje error
                    TempData["Mensaje"] = "No existe el usuario a restablecer el password";

                    //Se ubica al usuario dentro del formulario de restablecer
                    return View(pRestablecer);

                }

                
            }
            else
            {
                //Se muestra el mensaje de error mediante un alert
                TempData["Mensaje"] = "Datos incorrectos";

                //Se ubica al usuario dentro del formulario de restablecer
                return View(pRestablecer);
            }

            // -------------------------------------------------------------------------

            //Se toman los datos del usuario que debe restablecer la contraseña
            /*var temp = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == EmailRestablecer);

            //Se actualiza la clave del usuario
            temp.Password = pRestablecer.Password;

            //Se actualiza el estado de restablecer
            temp.Restablecer = 'N';

            //Se actualiza el usuario
            _context.Usuarios.Update(temp);

            //Se aplican los cambios
            await _context.SaveChangesAsync();

            //Se ubica al usuario dentro del formulario de login
            return RedirectToAction("Login", "Usuarios");*/

        }


        private Usuario ValidarUsuario(Usuario temp)
        {
            Usuario autorizado = null;

            //Se busca el email dentro del catalogo de usuarios
            var user = _context.Usuarios.FirstOrDefault(u => u.Email.Equals(temp.Email));

            if(user != null)//Se verifica si hay datos
            {
                //Se valida si la clave es correcta
                if (user.Password.Equals(temp.Password))
                {
                    autorizado = user;//Se toman los datos del usuarios
                }
            }

            return autorizado;
        }

        private bool VerificarRestablecer(Usuario temp)
        {
            bool verificado = false;

            //Se busca el email dentro del catalogo de usuarios
            var user = _context.Usuarios.FirstOrDefault(u => u.Email.Equals(temp.Email));

            if (user != null)//Se verifica si hay datos
            {
                //Se valida si la clave es correcta
                if (user.Restablecer == 'S')//Se verifica si se debe restablecer
                {
                    verificado = true;//Se marca que debe restablecer la clave temporal
                }
            }

            return verificado;// se retorna el valor para la variable control
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

        public async Task<IActionResult> Logout()
        {
            //Cierre de sesion
            await HttpContext.SignOutAsync();

            //Se ubica al usuario dentro del formulario inicial
            return RedirectToAction("Index", "Home");
        }

    }//Fin de la clase
}//Fin del namespace

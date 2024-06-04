using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Security.Claims;
using WebHotelBeach.Models;

namespace WebHotelBeach.Controllers
{
    public class ClienteController : Controller
    {
        
     
        //varible encargado de manejar la referencia para la API
        private API API;

        //variable que permite ejecutar todos los metodos publicos en la Ap
        private HttpClient client;

        //se instancia la base de datos (no la api) esto para poder controlar al usuario
        private readonly DbContextHotelesBeach _context = null;

        public ClienteController(DbContextHotelesBeach context)
        {
            //instancia de la base de datos
            _context = context;

            //instancia el objeto
            API = new API();

            //se incia la Api con su directorio base
            client = API.Iniciar();
        }

        //metodo index para ver la lista de usuarios
        public async Task<IActionResult> Index()
        {
            //genera una variable lista
            List<Cliente> cliente = new List<Cliente>();

            //una variable que busca en el listado de libros
            HttpResponseMessage response = await client.GetAsync("/Cliente/listado");

            //si la respuesta e codigo 200
            if (response.IsSuccessStatusCode)
            {
                //obtiene el contenido de la respuesta
                var resultado = response.Content.ReadAsStringAsync().Result;

                //pasa ese contenido a de Json a un formato leible po .NET
                cliente = JsonConvert.DeserializeObject<List<Cliente>>(resultado);
            }
            //envia a la lista con los clientes encontrados
            return View(cliente);
        }


        //----------------------------------------------------------------------------------------------
        //pagina create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }





        //-------------------------------------------------------------------------------------------
        //metodo Post del create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] Cliente cliente)
        {
            //inicializa con el ID en 0
            cliente.Id = 0;

            //le damos el rol en el sistema al cliente creado
            cliente.RolSistema = "cliente";


            //para hacer ciertas validaciones, vamos a la lista de clientes en la api
            var response = await client.GetAsync("/Cliente/listado");

            //hacemos una lista con clientes ya registrados
            List<Cliente> anterior = null;

            //valida si la respuesta fue correct (200)
            if (response.IsSuccessStatusCode)
            {
                //de ser correcta, leemos el contenido de la lista
                var contenido = await response.Content.ReadAsStringAsync();

                //se lo pasamos a una lista temporal
                anterior = JsonConvert.DeserializeObject<List<Cliente>>(contenido);
            }

            //con los datos obtenidos por esa lista de datos anteriores revisamos si ya existe el email y cedula
            var consultaEmail = anterior?.FirstOrDefault(c => c.Email == cliente.Email);
            var consultaCedula = anterior?.FirstOrDefault(c => c.Cedula == cliente.Cedula);

            //valida si existe clientes con esa cedula
            if( consultaCedula != null)
            {
                //de ser asi, no dejamos que el cliente digite una misma cedula
                TempData["Mensaje"] = "Ya existe un cliente, con la Cedula digitada";
                return View(cliente);
            }

            //valida si existe un cliente con esa correo
            if (consultaEmail != null)
            {
                //no se permiten correos duplicados
                TempData["Mensaje"] = "El correo electronico ya existe";
                return View(cliente);
            }

            //los if anteriores preferimos no hacerlos en una misma condicion
            //esto con la finalidad de que el usuario pueda saber con exactitud que esta duplicado

            //este es mensaje que se da si el numero de cedula fue incorrecto
            if(cliente.Nombre.Equals("Nombre no encontrado"))
            {
                TempData["Mensaje"] = "Por favor digite un numero de cedula correcto";
                return View(cliente);
            }


            //si ya todad las condiciones fueron correctas, se procede a la creacion del cliente en la API
            var subir = client.PutAsJsonAsync<Cliente>("/Cliente/Agregar", cliente);
            await subir;


            //se espera el resultado del Put
            var resultado = subir.Result;
            if (resultado.IsSuccessStatusCode)
            {
                //el resultado fue correcto

                //se procede con la creacion del usuario (Esto no esta en la API, En el documento no lo expecificaba)
                var usuario = new Usuario()
                {
                    Email = cliente.Email,
                    NombreCompleto = cliente.Nombre,
                    Password = cliente.Clave
                };

                // se guarda el usuario
                _context.Usuario.Add(usuario);

                // se instancia la variable email
                Email email = new Email();


                //se le envia un emnsaje de bienvenida a usuario
                email.Enviar(usuario);

                //se guardan los cambios
                await _context.SaveChangesAsync();

                //se redirecciona al login para que pueda iniciar sesion
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                //si no fue codigo 200, quiere decir que ocurrio un error al momento de agregarlo en la api
                TempData["Mensaje"] = "No se logro lamacenar el cliente";
                return View(cliente);
            }
        }





        //-------------------------------------------------------------------------------------------
        //vista del Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //llamada al objeto cliente
            var cliente = new Cliente();

            //metodo para intentar buscar el cliente por medio del id
            HttpResponseMessage responseMessage = await client.GetAsync($"/Cliente/Buscar/{id}");

            //valida si se logro encontrar o no
            if (responseMessage.IsSuccessStatusCode)
            {
                //lee el contenido de la respuesta
                var resultado = responseMessage.Content.ReadAsStringAsync().Result;

                //se llenan los datos del objeto conforme a los del obtenido con el metodo
                cliente = JsonConvert.DeserializeObject<Cliente>(resultado);


            }
            //se envia a la vista
            return View(cliente);
        }



        //------------------------------------------------------------------------------------
        //metodo post del delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCliente(int id)
        {

            //objeto temporal, se utiliza este metodo para buscar al cliente con ese id
            var cliente = await _context.Cliente.FindAsync(id);

            //metodo para eliminar ese cliente desde la api
            HttpResponseMessage responseMessage = await client.DeleteAsync($"/Cliente/Eliminar/{id}");

            //espera el resultado de la operacion
            if (responseMessage.IsSuccessStatusCode)
            {//respuesta correcta
                
                //si ese cliente que se encontro no es null
                if (cliente != null)
                {

                    //compara todos los paquetes asociados a ese cliente, la variable en comun es el correo
                    var paquetes = _context.Paquete.Where(p => p.EmailUsuario == cliente.Email).ToList();
                    
                    //elimina todos los paquetes asociados al cliente
                    if (paquetes.Count > 0)
                    {
                        _context.Paquete.RemoveRange(paquetes);

                        await _context.SaveChangesAsync();
                    }

                    //compara al usuario asociado al cliente,
                    var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == cliente.Email);
                    if (usuario != null)
                    {
                        //Elimina al usuario asociado al cliente
                        //recordemos que es una tabla aparte
                        _context.Usuario.Remove(usuario);

                        await _context.SaveChangesAsync();

                    }


                }
            }

            //si el que esta eliminando la cuenta es el cliente
            if (User.FindFirstValue("Rol").Equals("cliente"))
            {
                //se cierra la sesion y lo envia al home
                await HttpContext.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {//si es el admin, lo envia a la lista de usuarios
                return RedirectToAction("Index");
                //NOTA: el admin no se puede borrar a si mismo
                // si el esta aqui es porque esta borrando a un cliente, no a el
            }
            
        }




        //-------------------------------------------------------------------------------------
        //GET del edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //se instancia el objeto
            var cliente = new Cliente();

            //busca en la api un cliente on ese id
            HttpResponseMessage responseMessage = await client.GetAsync($"/Cliente/Buscar/{id}");

            //espera que la respuesta sea correcta
            if (responseMessage.IsSuccessStatusCode)
            {
                //respuesta correcta

                //se guarda el contenido de la respuesta
                var resultado = responseMessage.Content.ReadAsStringAsync().Result;

                //se cambia el formato de la respuesta y se pasa al objeto =
                cliente = JsonConvert.DeserializeObject<Cliente>(resultado);


            }
            //lleva a la vista de editar
            return View(cliente);
        }




        //------------------------------------------------------------------------------------
        //POST del Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind] Cliente cliente)
        {

            //para evitar conflictos se le vuelve a dar el rol a cliente en el edit
            cliente.RolSistema = "cliente";


            //se intena subir los datos modificados a la api
            var subir = client.PostAsJsonAsync("/Cliente/Modificar", cliente);
            await subir;

            //se espera el resultado de la api
            var resultado = subir.Result;
            if (resultado.IsSuccessStatusCode)
            {//respuesta correcta

                //se recorre la base buscando al usuario con el mismo email del cliente
                var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == cliente.Email);

                if (usuario == null)
                {//no se enontro el usuario
                    TempData["Mensaje"] = "Usuario no encontrado";
                    return View(cliente);
                }

                // Actualizar los datos del usuario si la edición del cliente fue exitosa
                usuario.NombreCompleto = cliente.Nombre;
                usuario.Password = cliente.Clave;

                //se actualizan los datos del usuario
                _context.Usuario.Update(usuario);
                await _context.SaveChangesAsync();

                //se redireccion al cliente a la vista de detalles del mismo
                return RedirectToAction("Details", "Cliente", new { id = cliente.Id });
            }
            else
            {// no se encotnro a ningun client con esos datos

                TempData["Mensaje"] = "Datos incorrectos";
                return View(cliente);
            }

            //NOTA: el usuario admin no se modifica
        }




        //------------------------------------------------------------------------------------
        //GET de details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            //se instancia un objeto en  blanco
            Cliente cliente = new Cliente();

            //se busca el cliente ene la api
            HttpResponseMessage responseMessage = await client.GetAsync($"/Cliente/Buscar/{id}");

            //se espera la respuesta
            if (responseMessage.IsSuccessStatusCode)
            {
                //se obtine el contenido de la respuesta
                var resultado = responseMessage.Content.ReadAsStringAsync().Result;

                //se le quita el formato json y se pasa al objeto
                cliente = JsonConvert.DeserializeObject<Cliente>(resultado);

            }
            //envia a la vista con los datos obtenidos
            return View(cliente);

        }



    }//cierre class
}//cierre namespace

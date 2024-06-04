//Uso del objeto que esta en el model
using ApiHotelBeach.Model;

//Librerias a utilizar
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiHotelBeach.Controllers
{
    [ApiController]//permite que nuestro controlador reciba peticiones de los verbos Http
    [Route("[Controller]")]//permite exponer las rutas para cada metodo escrito
    public class ClienteController : Controller
    {
        //se crea una variable privada para almacenar la referencia al ORM
        private readonly DbContextHotelesBeach _context = null;

        //se crea un constructor para inicializar la variable
        public ClienteController(DbContextHotelesBeach pContext)
        {
            _context = pContext; //se asigna la referencia ORM
        }

        // -------------------- METODO LISTADO --------------------

        //Metodo para listar todos los clientes
        [HttpGet("listado")]
        public async Task<List<Cliente>> Listado()
        {
            //se crea una variable para almacenar la lista de clientes
            var lista = await _context.Cliente.ToListAsync();
            //se retorna la lista
            return lista;
        }

        // -------------------- METODO BUSCAR --------------------

        //Metodo para buscar un cliente por su ID
        [HttpGet("Buscar/{id}")]
        public async Task<Cliente> Buscar(int id)
        {
            //se crea una variable para almacenar el cliente encontrado
            var temp = await _context.Cliente.FirstOrDefaultAsync(x => x.Id == id);
            //se retorna el cliente encontrado
            return temp == null ? new Cliente() : temp;
        }

        // -------------------- METODO ELIMINAR --------------------

        //Metodo para eliminar un cliente por medio de su ID
        [HttpDelete("Eliminar/{id}")]
        public async Task<string> Eliminar(int id)
        {
            //se crea una variable para almacenar el mensaje de respuesta
            string mensaje = "Error, No se logro eliminar el cliente con el ID " + id;

            try
            {
                //se busca el cliente por su ID
                var temp = await _context.Cliente.FirstOrDefaultAsync(x => x.Id == id);
                //Validacion para saber i el cliente existe
                if (temp != null)
                {
                    //se elimina el cliente
                    _context.Cliente.Remove(temp);
                    //se guardan los cambios
                    await _context.SaveChangesAsync();
                    //se actualiza el mensaje de respuesta
                    mensaje = "El Cliente: " + temp.Nombre + " fue eliminado correctamente";
                }
            }
            //en caso de error se captura la excepcion
            catch (Exception ex)
            {
                //en caso que se genera un erro se almacena al mensjae de la info de error
                mensaje += ex.InnerException.Message;
            }
            //se retorna el mensaje
            return mensaje;
        }

        // -------------------- METODO AGREGAR --------------------

        //Metodo para agregar un cliente
        [HttpPut("Agregar")]
        public async Task<string> Agregar(Cliente pCliente)
        {
            //se crea una variable para almacenar el mensaje de respuesta
            string mensaje = ""; 
            try
            {
                //se agrega el cliente
                _context.Cliente.Add(pCliente);
                //se guardan los cambios
                await _context.SaveChangesAsync();
                //se actualiza el mensaje de respuesta
                mensaje = $"Cliente {pCliente.Nombre} almacenado correctamente...";

            }
            //en caso de error se captura la excepcion
            catch (Exception ex)
            {
                //en caso que se genera un error se almacena al mensaje de la informacion del error
                mensaje = "Error no se logro almacenar el cliente \n" +
                    ex.InnerException.Message;
            }
            //se retorna el mensaje
            return mensaje;
        }

        // -------------------- METODO MDIFICAR --------------------

        //Metodo para modificar un cliente
        [HttpPost("Modificar")]
        public async Task<string> Modificar(Cliente pCliente)
        {
            //se crea una variable para almacenar el mensaje de respuesta
            string mensaje = $"No se logro aplicar los cambios al cliente {pCliente.Nombre}";

            try
            {
                //se actualiza el cliente
                _context.Cliente.Update(pCliente);
                //se guardan los cambios
                await _context.SaveChangesAsync();
                //se actualiza el mensaje de respuesta
                mensaje = $"Cambios aplicados correctamente al cliente {pCliente.Nombre}";

            }
            catch (Exception ex)
            {
                //en caso que se genera un error se almacena al mensaje de la informacion del error
                mensaje += "\n Error, " + ex.InnerException.Message;
            }
            //se retorna el mensaje
            return mensaje;
        }

    }//cierre class
}//cierre namespace

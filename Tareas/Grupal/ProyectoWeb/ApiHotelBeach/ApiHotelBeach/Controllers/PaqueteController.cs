//Uso del objeto que esta en el model
using ApiHotelBeach.Model;

//Librerias a utilizar
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiHotelBeach.Controllers
{
    [ApiController]//permite que nuestro controlador reciba peticiones de los verbos Http
    [Route("[Controller]")]//permite exponer las rutas para cada metodo escrito
    public class PaqueteController : Controller
    {
        //se crea una variable privada para almacenar la referencia al ORM
        private readonly DbContextHotelesBeach _context = null;

        //se crea un constructor para inicializar la variable
        public PaqueteController(DbContextHotelesBeach pContext)
        {
            _context = pContext; //se asigna la referencia ORM
        }

        // -------------------- METODO LISTADO --------------------

        //Metodo para listar todos los paquetes
        [HttpGet("listado")]
        public async Task<List<Paquete>> Listado()
        {
            //se crea una variable para almacenar la lista de paquetes
            var lista = await _context.Paquete.ToListAsync();
            //se retorna la lista
            return lista;
        }

        // -------------------- METODO BUSCAR --------------------

        //Metodo para buscar un paquete por su ID
        [HttpGet("Buscar/{id}")]
        public async Task<Paquete> Buscar(int id)
        {
            //se crea una variable para almacenar el paquete encontrado
            var temp = await _context.Paquete.FirstOrDefaultAsync(x => x.Id == id);
            //se retorna el paquete encontrado
            return temp == null ? new Paquete() : temp;
        }

        // -------------------- METODO ELIMINAR --------------------

        //Metodo para eliminar un paquete por medio de su ID
        [HttpDelete("Eliminar/{id}")]
        public async Task<string> Eliminar(int id)
        {
            //se crea una variable para almacenar el mensaje de respuesta
            string mensaje = "Error, No se logro eliminar el paquete ";

            try
            {
                //se busca el paquete por su ID
                var temp = await _context.Paquete.FirstOrDefaultAsync(x => x.Id == id);
                //Validacion para saber i el paquete existe
                if (temp != null)
                {
                    //se elimina el paquete
                    _context.Paquete.Remove(temp);
                    //se guardan los cambios
                    await _context.SaveChangesAsync();
                    //se almacena el mensaje de respuesta
                    mensaje = "El Paquete fue eliminado correctamente";
                }
            }
            // Captura de la excepcion
            catch (Exception ex)
            {
                //en caso que se genera un erro se almacena al mensjae de la info de error
                mensaje += ex.InnerException.Message;
            }
            //se retorna el mensaje
            return mensaje;
        }

        // -------------------- METODO AGREGAR --------------------

        //Metodo para agregar un paquete
        [HttpPut("Agregar")]
        public async Task<string> Agregar(Paquete pPaquete)
        {
            //se crea una variable para almacenar el mensaje de respuesta
            string mensaje = "";
            try
            {
                //se agrega el paquete
                _context.Paquete.Add(pPaquete);
                //se guardan los cambios
                await _context.SaveChangesAsync();
                //se actualiza el mensaje de respuesta
                mensaje = "Paquete almacenado correctamente...";

            }
            //Captura de excepcion
            catch (Exception ex)
            {
                //en caso de error se captura la excepcion
                mensaje = "Error no se logro almacenar el paquete \n" +
                    ex.InnerException.Message;
            }
            //se retorna el mensaje
            return mensaje;
        }

        // -------------------- METODO MODIFICAR --------------------

        //Metodo para modificar un paquete
        [HttpPost("Modificar")]
        public async Task<string> Modificar(Paquete pPaquete)
        {
            //se crea una variable para almacenar el mensaje de respuesta
            string mensaje = $"No se logro aplicar los cambios al paquete";

            try
            {
                //se actualiza el paquete
                _context.Paquete.Update(pPaquete);
                //se guardan los cambios
                await _context.SaveChangesAsync();
                //se actualiza el mensaje de respuesta
                mensaje = $"Cambios aplicados correctamente al paquete";

            }
            // Excepcion
            catch (Exception ex)
            {
                //en caso de error se captura la excepcion
                mensaje += "\n Error, " + ex.InnerException.Message;
            }
            //se retorna el mensaje
            return mensaje;
        }//fin del metodo Modificar

    }//fin de la clase
}//fin del namespace

using AppBiliotecaWebPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppBiliotecaWebPI.Controllers
{
    [ApiController] //Permite que nuestro controlador reciba peticiones de los verbos Http 
    [Route("[Controller]")] //Permite exponer las rutas para cada metodo escrito
    public class LibrosController : Controller
    {
        //Variable para manejar la referencia del ORM
        private readonly DbContextBiblioteca _context = null;

        /// <summary>
        /// Constructor con parametros recibe la referencia del ORM
        /// </summary>
        /// <returns>
        /// 

        public LibrosController(DbContextBiblioteca context)
        {
            _context = context; //Se asigna la referencia
        }

        [HttpGet("listado")]
        public async Task<List<Libro>> Listado() {

            //Se carga la lista de libros almacenados en la Db por medio del ORM
            var lista = await _context.Libros.ToListAsync();

            //Se envia la lista
            return lista;
        }

        //BUSCAR
        [HttpGet("Buscar/{id}")]
        public async Task<Libro> Buscar(int id){

            //Se busca el libro por medio del ISBN
            var temp = await _context.Libros.FirstOrDefaultAsync(x => x.ISBN == id);

            //Se utiliza una expresion lambda para controlar si el valor es nulo
            return temp == null ? new Libro() : temp; //Se devuelve el libro

        }

        //ELIMINAR
        [HttpDelete("Eliminar/{id}")]

        public async Task<string> Eliminar(int id) {

            string mensaje = "Error, no se logro eliminar el libro con el ISBN: " + id;

            try { 
                var temp = await _context.Libros.FirstOrDefaultAsync(x => x.ISBN == id);
                if(temp != null)
                {
                    //Se elimina el libro
                    _context.Libros.Remove(temp);

                    //Se aplican los cambios
                    await _context.SaveChangesAsync();

                    //Se indica un mensaje de exito
                    mensaje = "Libro " + temp.Titulo + " fue eliminado correctamente";

                }

            }//Fin try
            catch(Exception ex)
            {
                //En caso que se genere un error se almacena un mensaje la info de error
                mensaje += ex.InnerException.Message;

            }//Fin catch

            return mensaje;

        }

        //PROGRAMACION 10/5/25

        //Metodo encargado de agregar un libro
        [HttpPut("Agregar")]

        public async Task<string> Agregar(Libro libro) {

            string mensaje = "";//Variable para almacenar el texto de mensaje

            try { 

            //Se agrega el libro al catalogo
            _context.Libros.Add(libro);

            //se aplican los cambios
            await _context.SaveChangesAsync();

                //Se indica al usuario que se almacena con exito
                mensaje = $"libro: {libro.Titulo} almacenado correctamente....";

            }
            catch (Exception ex){

                //Se indica los datos del error en caso de que exista
                mensaje = $"Error no se logro almacenar el libro: {libro.Titulo}\n" +
                    ex.InnerException.Message;
            }
            return mensaje;
        
        }

        [HttpPut("Modificar")]

        public async Task<string> Modificar(Libro libro) {

            //Se muestra el mensaje al usuario
            string mensaje = $"No se logro aplicar los cambios al libro: {libro.ISBN}";

            try
            {
                //Se realiza los cambios
                _context.Libros.Update(libro);

                //Se aplican los cambios
                await _context.SaveChangesAsync();

                //Se indica un mensaje al usuario
                mensaje = $"Cambios aplicados correctamente al libro: {libro.Titulo}";

            }
            catch (Exception ex)
            {
                mensaje += "\n Error, " + ex.InnerException.Message;
            }
            return mensaje;

        }

    }//Fin clase
}//Fin namespace

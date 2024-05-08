using AppBibliotecaWebG1.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppBibliotecaWebG1.Controllers
{
    public class GalleryController : Controller
    {
        //Variable que permite manejar la referencia la ORM
        private readonly DbContextBiblioteca _context = null;

        /// <summary>
        /// Constructor que recibe la instancia del ORM
        /// </summary>
        /// <param name="pContext"></param>

        public GalleryController(DbContextBiblioteca pContext)
        { 
            _context = pContext;//Se asigna la referencia
        }

        public IActionResult Gallery()
        {
            //Se toma la lista de libros
            var listado = _context.Libros.ToList();

            //Se envía la lista de libros al front end
            return View(listado);
        }
    }
}

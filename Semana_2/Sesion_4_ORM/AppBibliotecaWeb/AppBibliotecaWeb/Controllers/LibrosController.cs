using Microsoft.AspNetCore.Mvc;
using AppBibliotecaWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AppBibliotecaWeb.Controllers
{
    public class LibrosController : Controller
    {
        //Variable para manejar el ORM entity framework core
        private readonly DbContextBiblioteca _context;

        /// <summary>
        /// Constructor con parametros del controlador
        /// </summary>
        /// <param name="context"></param>

        public LibrosController(DbContextBiblioteca context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //Se usa el listado para leer todos los datos del catalogo de libros
            var listado = await _context.Libros.ToListAsync(); 
            
            //se envia como parametros al front end la lista de libros
            return View(listado);
        }

        /*public IActionResult Index()
        {
            return View();
        }*/
    }
}

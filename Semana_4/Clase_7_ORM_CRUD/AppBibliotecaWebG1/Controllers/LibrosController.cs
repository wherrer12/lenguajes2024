using Microsoft.AspNetCore.Mvc;

//Se referencia el ORM
using AppBibliotecaWebG1.Models;
using Microsoft.EntityFrameworkCore;

namespace AppBibliotecaWebG1.Controllers
{
    public class LibrosController : Controller
    {

        //Variable que permite manejar la referencia del contexto
        private readonly DbContextBiblioteca _context = null;

        /// <summary>
        /// Constructor con parametros que recibe la referencia del ORM
        /// </summary>
        /// <param name="context"></param>
        /// 
        public LibrosController(DbContextBiblioteca context)
        {
            _context = context;//Se asigna la referencia al contexto
        }

        public async Task<IActionResult> Index() //asincrinono por  ir en la bd
        {

            //Se declara la variable lista
            //Por medio de ORM se lee la informacion de todos los libros en la BD
            var listado = await _context.Libros.ToListAsync();

            return View(listado);//Se envia el listado al front end
        }

        //Metodo encargado para almacenar libro
        [HttpGet]
        public IActionResult Create()//Metodo encargado de mostrar el front end para crear un libro
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind] Libro libro)
        {
            if (libro == null)//Se valida que el objeto libro tenga datos
            {
                return View();//Como no hay datos, se deja al usuario dentro del formulario create
            }
            else { 
                //Si hay datos alamcenados en el libro
                _context.Libros.Add(libro);//Se agrega el libro al contexto

                //Se aplican cambios en la BD
                await _context.SaveChangesAsync();

                //Ubicamos al usuario dentro del listado libros
                return RedirectToAction("Index");
            
            }
        }

    }
}

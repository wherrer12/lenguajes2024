using Microsoft.AspNetCore.Mvc;

//Se importa la capa model
using AppBilbliotecaWeb.Models;

namespace AppBilbliotecaWeb.Controllers
{
    public class LibrosController : Controller
    {

        //Declaracion variable de tipo List
        private static List<Libro> listado = null;

        //Constructor
        public LibrosController()
        {
            //se pregunta si la lista esta vacia
            if (listado == null)
            {
                //Se instancia la lista
                listado = new List<Libro>();

                //se agrega libro al listado
                listado.Add(new Libro()
                {
                    ISBN = 44,
                    Titulo = "Programacion IV",
                    PrecioVenta = 25600,
                    FechaPublicacion = DateTime.Now,
                    Editorial = "Imprenta La Violeta",
                    Foto = "ND",
                    Estado = 'A'
                });

                listado.Add(new Libro()
                {
                    ISBN = 46,
                    Titulo = "Estructura datos",
                    PrecioVenta = 25300,
                    FechaPublicacion = DateTime.Now,
                    Editorial = "Puntarenas",
                    Foto = "ND",
                    Estado = 'A'
                });

                listado.Add(new Libro()
                {
                    ISBN = 91,
                    Titulo = "Bases de datos Oracle",
                    PrecioVenta = 65300,
                    FechaPublicacion = DateTime.Now,
                    Editorial = "Imprenta La Violeta",
                    Foto = "ND",
                    Estado = 'A'
                });

            }//Fin if

        }//Cierre constructor

        ///<summary>
        ///Primer metodo que ejecuta el controlador
        /// </summary>
        /// <returns></returns>

        public IActionResult Index()
        {
            //Se envia al front-end el listado de libros
            return View(listado);
        }//Cierre view index

        //Proceso para registrar un libro
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }//Cierre create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind]Libro libro)
        {
            //Se valida que no este vacio el libro
            if (libro == null)
            {
                return View();
            }
            else {
                //si tiene datos lo agregamos al listado
                listado.Add(libro);

                //Una vez guardado ubicamos al usuario dentro del listado libros
                return RedirectToAction("Index", "Libros");
            }

        }

        //Metodos para realizar los procesos de editar un libro
        [HttpGet]
        public IActionResult Edit(int id) { //ID parametro que almacena el valor del ISBN
            //Se busca el libro dentro de listado
            var temp = listado.FirstOrDefault(j => j.ISBN == id);

            //se envia al front end los datos del libro
            return View(temp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Libro libro) {
            if (libro == null)
            {
                return View();//Se valida que el objeto tenga datos
            }
            else {
                //Si tiene datos se valida el ID del libro a modificar
                if (id != libro.ISBN)
                {
                    return View(libro);
                }
                else {
                    //Se busca el libro a modificar
                    var temp = listado.FirstOrDefault(j => j.ISBN == id);

                    //Se elimina el libro viejo
                    listado.Remove(temp);

                    //Se agrega el libro con los nuevos datos
                    listado.Add(libro);

                    //Se ubica al usuario dentro del listado de libros
                    return RedirectToAction("Index", "Libros");
                }
            }
        
        }//Fin metodo edit post

        //Procesos para eliminar libro
        [HttpGet]
        public IActionResult Delete(int id) {

            //Se busca el libro por medio del ISBN
            var temp = listado.FirstOrDefault(j=>j.ISBN == id);

            //Se envia los datos
            return View(temp);

        }//Cierre delete get

        [HttpPost]
        public IActionResult Delete(int? id) {

            //Se busca el libro por medio del isbn
            var temp = listado.FirstOrDefault(y => y.ISBN == id);

            //Se elimina el libro
            listado.Remove(temp);

            //Se ubica al usuario dentro del listado de libros
            return RedirectToAction("Index","Libros");

        }//Cierre delete post

        //Proceso para details
        [HttpGet]
        public IActionResult Details(int id) {
            //Se busca por medio del ISBN
            var temp = listado.FirstOrDefault(i => i.ISBN == id);

            //Se utiliza lambda para c#
            return temp != null ? View(temp) : View(null); //Parte verdaero, parte falsa
        }//Fin get details

    }//Cierre controller
}//Cierre namespace

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

        public async Task<IActionResult> Create(List<IFormFile> files, [Bind] Libro libro)
        {
            if (libro == null)//Se valida que el objeto libro tenga datos
            {
                return View();//Como no hay datos, se deja al usuario dentro del formulario create
            }
            else { 

                //Se asigna cero para generar el ISBN automatico
                libro.ISBN = 0;

                //Se pregunta si tiene la foto
                if(files.Count > 0)
                {
                    //lugar donde se almacena la foto
                    string filePath = @"wwwroot\css\img\";

                    //variable para construir el nombre de la foto
                    string filename = "";

                    //Se leen las fotos adjuntas al formulario
                    foreach(var formFile in files)
                    {
                        //Se valida el tamanio para la foto
                        if (formFile.Length > 0)
                        {
                            //Se construye el nombre de la foto
                            filename = libro.Titulo + "_" + formFile.FileName;

                            //Se quitan los espacios en blanco dentros del nombre de la foto
                            filename = filename.Replace(" ", "_");

                            //Mejorar la calidad de la ruta
                            filename = filename.Replace("#", "_");

                            //Se indica la ruta fisica donde se almacena la foto
                            filePath += filename;
                            
                            //Se copia la foto dentro de la carpeta
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                //Se copia la foto
                                await formFile.CopyToAsync(stream);

                                //Ahora se indica a la base de datos donde se ubica la foto
                                libro.Foto = @"/css/img/" + filename;

                            }//Cierre del using

                        }//Cierre del if

                    }//Fin del foreach

                }

                //Si hay datos alamcenados en el libro
                _context.Libros.Add(libro);//Se agrega el libro al contexto

                //Se aplican cambios en la BD
                await _context.SaveChangesAsync();

                //Ubicamos al usuario dentro del listado libros
                return RedirectToAction("Index");
            
            }
        }

        //Metodo para el proceso de eliminacion de un libro
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //Buscar el libro a eliminar por medio del ORM
            var temp = await _context.Libros.FirstOrDefaultAsync(r => r.ISBN == id); 

            //Se envian los datos al front end
            return View(temp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int? id)
        {
            //Se busca por ISBN el libro a eliminar
            var temp = await _context.Libros.FirstOrDefaultAsync(r => r.ISBN == id);

            //Se valida si tiene datos
            if (temp != null)
            {

                //Se elimina el libro
                _context.Libros.Remove(temp);

                //Se aplican los cambios en la BD
                await _context.SaveChangesAsync();

                //Se redirige al usuario al listado de libros
                return RedirectToAction("Index");
                
            }
            else
            {
                return NotFound();//Error 404, recurso no disponible
            }
            
        }

        //Metodo encargado de mostrar los datos para un libro especifico
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            //Se busca el libro por ISBN
            var temp = await _context.Libros.FirstOrDefaultAsync(r => r.ISBN == id);

            //Se envian los datos al front end
            return View(temp);
        }

        //Metodo encargado de realizar el proceso de editar de los datos de un libro
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //Se busca el libro por ISBN
            var temp = await _context.Libros.FirstOrDefaultAsync(r => r.ISBN == id);

            //Se envian los datos del libro al front end
            return View(temp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind] Libro pLibro)
        {
            //Se valida el ID
            if (id == pLibro.ISBN)
            {
                //Se busca el libro anterior con sus datos
                var temp = await _context.Libros.FirstOrDefaultAsync(r => r.ISBN == id);

                //Se elimina el libro
                _context.Libros.Remove(temp);

                //Se aplican los cambios en la BD
                _context.Libros.Add(pLibro);

                //Se aplican los cambios en la BD
                await _context.SaveChangesAsync();

                //Se redirige al usuario dentro del listado de libros
                return RedirectToAction("Index");

            }
            else
            {

                return NotFound();//Error 404, recurso no disponible

            }
        }   

    }//Fin de la clase
}//Fin namespace

using Microsoft.AspNetCore.Mvc;
using AppWebBibliotecaConsumoG1.Models;
using Newtonsoft.Json;

namespace AppWebBibliotecaConsumoG1.Controllers
{
    public class LibrosController : Controller
    {
        //Variable encargada de manejar la referencia para la API
        private LibrosAPI librosAPI;

        //Variable que permite ejecutar todos los metodos publicos en la API
        private HttpClient client;

        public LibrosController()
        {
            //Se instancia el objeto
            librosAPI = new LibrosAPI();

            //Se inicia la API con su directorio base
            client = librosAPI.Iniciar();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Se instancia una lista de libros vacia
            List<Libro> libros = new List<Libro>();

            //Se ejecuta el metodo de la API que devuelve el listado de libros
            HttpResponseMessage response = await client.GetAsync("Libros/Listado");

            //Se valida si la respuesta fue correcta
            if (response.IsSuccessStatusCode)
            {
                //Se lee los datos en formato JSON
                var resultado = response.Content.ReadAsStringAsync().Result;

                //Se convierten los datos en una lista de objetos
                libros = JsonConvert.DeserializeObject<List<Libro>>(resultado);
            }

            //Ojo se envia la lista de libros a la vista (Frontend)
            return View(libros);
        }

        //Metodo encargado para mostrar libro
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind] Libro libro)
        {
            //Se asigna el valor 0 al ISBN porque es autoincrementable
            libro.ISBN = 0;

            //Se ejecuta el metodo para la API pero se envia como parametro el objeto libro con la informacion
            var subir = client.PutAsJsonAsync<Libro>("Libros/Agregar", libro);
            await subir;//Se ejecuta la tarea

            var resultado = subir.Result;//Se toma el resultado

            if (resultado.IsSuccessStatusCode) // Se valida si el proceso fue exitoso
            {
                return RedirectToAction("Index");//Se ubica al usuario dentro del listado de libros 
            }
            else
            {
                //Se genero un error
                TempData["Mensaje"] = "No se logro almacenar el libro";//Se indica el mensaje de error
                return View(libro);//Se ubica al usuario dentro del formulario crear para que valide los datos
            }
        }

        //Metodo encargado de realizar el proceso de eliminacion de un libro
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //Se instancia un objeto libro pero con los datos vacios
            var libro = new Libro();

            //Se ejecuta el metodo buscar por medio del ID para obtener los datos del libro a eliminar
            HttpResponseMessage responseMessage = await client.GetAsync($"Libros/Buscar/{id}");

            //Se valida si el proceso de consulta fue exitoso
            if (responseMessage.IsSuccessStatusCode)
            {
                //Se leen los datos en formato JSON
                var resultado = responseMessage.Content.ReadAsStringAsync().Result;

                //Los datos se convierten en un objeto libro
                libro = JsonConvert.DeserializeObject<Libro>(resultado);
            }

            //Se envia el objeto al front end para mostrar los datos
            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteBook(int id)//Diferente por sobrecarga de nombre
        {
            //Se ejecuta el metodo eliminar se envia por parametro el ID del libro a eliminar
            HttpResponseMessage responseMessage = await client.DeleteAsync($"Libros/Eliminar/{id}");

            //Se ubica al usuario dentro del listado de libros
            return RedirectToAction("Index");

        }

        // Metodo encargado de editar los datos del libro
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //Se instancia un objeto libro pero con los datos vacios
            Libro libro = new Libro();

            //Se ejecuta el metodo buscar por medio del ID para obtener los datos del libro a editar
            HttpResponseMessage responseMessage = await client.GetAsync($"Libros/Buscar/{id}");

            //Se valida si el proceso de consulta fue exitoso
            if (responseMessage.IsSuccessStatusCode)
            {
                //Se leen los datos en formato JSON
                var resultado = responseMessage.Content.ReadAsStringAsync().Result;

                //Los datos se convierten en un objeto libro
                libro = JsonConvert.DeserializeObject<Libro>(resultado);
            }

            //Se envia el objeto al front end para mostrar los datos
            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind] Libro libro)
        {
            //Se ejecuta el metodo modificar que envia el objeto libro con los datos actualizados
            var subir = client.PostAsJsonAsync<Libro>($"Libros/Modificar/",libro);
            await subir;//Se ejecuta la tarea

            var resultado = subir.Result;//Se toma la respuesta

            if (resultado.IsSuccessStatusCode) // Se valida si el proceso fue exitoso
            {
                return RedirectToAction("Index");//Se ubica al usuario dentro del listado de libros 
            }
            else
            {
                //Se genero un error
                TempData["Mensaje"] = "Datos incorrectos";//Se indica el mensaje de error
                return View(libro);//Se ubica al usuario dentro del formulario crear para que valide los datos
            }
        }

        //Metodo encargado de mostrar los detalles de los datos
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            //Se instancia un objeto libro pero con los datos vacios
            Libro libro = new Libro();

            //Se ejecuta el metodo buscar en la api, se envia el ISBN como parametro
            HttpResponseMessage responseMessage = await client.GetAsync($"Libros/Buscar/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                //Se leen los datos en formato JSON
                var resultado = responseMessage.Content.ReadAsStringAsync().Result;

                //Se convierte los datos en un objeto libro
                libro = JsonConvert.DeserializeObject<Libro>(resultado);

            }
            //Se envia el objeto al front end para mostrar su informacion
            return View(libro);

        }

    }//Fin de la clase
}//Fin del namespace

using AppBibliotecaConsumoG1.Models;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace AppBibliotecaConsumoG1.Controllers
{
    public class LibrosController : Controller
    {
        private LibrosAPI librosAPI;

        private HttpClient client;

        public LibrosController() {

            librosAPI = new LibrosAPI();

            client = librosAPI.Iniciar();

        }

        public async Task<IActionResult> Index()
        {
            List<Libro> listado = new List<Libro>();

            HttpResponseMessage responseMessage = await client.GetAsync("/Libros/Listado");

            if(responseMessage.IsSuccessStatusCode) { 
            
                var resultados = responseMessage.Content.ReadAsStringAsync().Result;

                listado = JsonConvert.DeserializeObject<List<Libro>>(resultados);

            }

            return View(listado);
        }
    }
}

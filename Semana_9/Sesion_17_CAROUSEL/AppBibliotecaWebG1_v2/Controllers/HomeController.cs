using AppBibliotecaWebG1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AppBibliotecaWebG1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //Variable para almacenar los datos del archivo JSON
        public static TipoCambio tipoCambio = null;

        //Variable para manejar la referencia de la API
        private TipoCambioAPI tipoCambioAPI = null;

        //Variable para manejar las respuestas para la API
        private HttpClient clientTipoCambio = null;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            //Se instancia el objeto tipo cambio API
            tipoCambioAPI = new TipoCambioAPI();

            //Se inicializa la API
            clientTipoCambio = tipoCambioAPI.Iniciar();

            //Se utiliza el metodo para extraer el tipo de cambio
            ExtraerTipoCambio();

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async void ExtraerTipoCambio()
        {
            try { 
                
                //Se consume el metodo para la API
                HttpResponseMessage response = await clientTipoCambio.GetAsync("tdc/tdc.json");

                //Se valida si todo fue correcto
                if (response.IsSuccessStatusCode) { 
                    
                    //Se realiza la lectura de los datos del formato JSON
                    var result= response.Content.ReadAsStringAsync().Result;

                    //Se convierte el JSON a un objeto tipo de cambio
                    tipoCambio = JsonConvert.DeserializeObject<TipoCambio>(result);

                }

            }
            catch (Exception ex)
            {
                
            }
        }

    }//Fin de la clase
}//Fin del namespace

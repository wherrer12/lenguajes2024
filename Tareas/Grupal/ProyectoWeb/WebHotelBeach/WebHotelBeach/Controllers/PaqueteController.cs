using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Net.Mail;
using WebHotelBeach.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace WebHotelBeach.Controllers
{
    public class PaqueteController : Controller
    {

        //para llamar a la API
        private API API;

        private HttpClient client;


        //llamada a la api
        public PaqueteController()
        {
            API = new API();

            client = API.Iniciar();
        }




        //---------------------------------------------------------------------------
        //Get de lista paquetes
        public async Task<IActionResult> Index()
        {
            // se instancia una lista de paquetes
            List<Paquete> paquete = new List<Paquete>();

            //se utiliza el metodo para traer la lista de paquetes
            HttpResponseMessage response = await client.GetAsync("/Paquete/listado");

            //se espera la respuesta
            if (response.IsSuccessStatusCode)
            {
                //se obtiene los datos de la rspuesta
                var resultado = response.Content.ReadAsStringAsync().Result;

                //se envian esos datos a la lista
                paquete = JsonConvert.DeserializeObject<List<Paquete>>(resultado);
            }
            //se envia a la vista
            return View(paquete);
        }





        //--------------------------------------------------------------------------
        //GET del create
        [HttpGet]
        public IActionResult Create()
        {
            //verifica si ya se incio sesion
            if (User.Identity.IsAuthenticated)
            {
                //de ser asi, se envia al create
                return View();
            }
            else
            {
                //caso contrario,primeramente se pide el iniciar sesion
                TempData["Mensaje"] = "Antes de Comprar un Paquete, por favor inicia Sesion";

                return RedirectToAction("Login","Usuario");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] Paquete paquete)
        {
            //se inicializa el id en 0
            paquete.Id = 0;
            
            //se busca en el listado de paquetes
            var responsePackages = await client.GetAsync("/Paquete/listado");
            //lista temporal de paquetes
            List<Paquete> paquetes = null;

            //espera la respuesta
            if (responsePackages.IsSuccessStatusCode)
            {
                //guarda el cntenido de la respuesta
                var contenido = await responsePackages.Content.ReadAsStringAsync();

                //convierte el JSON de la respuesta
                paquetes = JsonConvert.DeserializeObject<List<Paquete>>(contenido);
            }

            //una vez obtenida la lista de paquetes
            //se busca que no hayan paqutes con la fecha a registrar
            if (paquetes != null && paquetes.Any(p => p.FechaReserva.Date == paquete.FechaReserva.Date))
            {
                //no permite guardar dos con la misma fecha
                TempData["Mensaje"] = "Ya existe un paquete con la misma fecha de reserva.";
                return View(paquete);
            }

            //si esta guardando un paqute con la forma de pago cheque
            //y no esta digitando uno
            if (paquete.formadePago.Equals("Cheque") && paquete.numeroCheque == 0)
            {
                //no permite guarda con cheque vacio
                TempData["Mensaje"] = "Por favor digite un numero de cheque si desea pagar con uno";
                return View(paquete);
            }

            //se intenta agregar el paquete
            var subir = await client.PutAsJsonAsync<Paquete>("/Paquete/Agregar", paquete);
           

                //espera la respuesta de la api
                if (subir.IsSuccessStatusCode)
                {//respuesta correcta

                    //se busca en el listado de clientes
                    var response = await client.GetAsync("/Cliente/listado");

                    //se crea una lista temporar
                    List<Cliente> clientes = null;

                    //s espera la respuesta de la busqueda de listado
                    if (response.IsSuccessStatusCode)
                    {
                        //se obtiene el contenido de la respuesta
                        var contenido = await response.Content.ReadAsStringAsync();
                        //se convierte el JSON
                        clientes = JsonConvert.DeserializeObject<List<Cliente>>(contenido);
                    }

                    //con la lista de clientes, busca a cliente con el que se esta creando el paquete 
                    var cliente = clientes?.FirstOrDefault(c => c.Email == paquete.EmailUsuario);

                    if (cliente != null)
                    {   //si se encuentra a ese cliente

                        //se envia al metodo de api gometa para obtener el cambio a colones
                        decimal montoTotalCRC = await Cambio(paquete.montoTotal);

                        //se instancia el objeto email
                        Email email = new Email();

                        //se envia un email con la factura en PDF
                        email.EnviarFactura(cliente, paquete, montoTotalCRC);
                    }

                    //se redirecciona a la propia lista de paquetes
                     return RedirectToAction("Index","Paquete");

                } 
                else
                {
                //respuesta incorrecta

                    TempData["Mensaje"] = "No se logro lamacenar el paquete";
                    return View(paquete);
                }

            }


        //-------------------------------------------------------------------------
        //GET del delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //se instancia un objeto
            var paquete = new Paquete();

            //se trata de recibir una respuesta correcta de la api
            HttpResponseMessage responseMessage = await client.GetAsync($"/Paquete/Buscar/{id}");

            //espera la respuesta
            if (responseMessage.IsSuccessStatusCode)
            {
                //obtiene el contenido de la rspuesta
                var resultado = responseMessage.Content.ReadAsStringAsync().Result;

                //lo transforma para enviarlo a la vista
                paquete = JsonConvert.DeserializeObject<Paquete>(resultado);


            }
            //se envia a la vista
            return View(paquete);
        }




        //-----------------------------------------------------------------------
        //POST del delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePaquete(int id)
        {
            //se elimina el paquete imediatamente de la api
            HttpResponseMessage responseMessage = await client.DeleteAsync($"/Paquete/Eliminar/{id}");

            //se redirecciona a la lista de paquetes
            return RedirectToAction("Index");
        }


        //-------------------------------------------------------------------------
        //GET del edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // se instacia un objeto paquete
            var paquete = new Paquete();

            //se busca el onbejto con el id deseado
            HttpResponseMessage responseMessage = await client.GetAsync($"/Paquete/Buscar/{id}");

            //espera una respuesta efecitva
            if (responseMessage.IsSuccessStatusCode)
            {
                //se obtiene los datos de la respuesta
                var resultado = responseMessage.Content.ReadAsStringAsync().Result;

                //se convierte el JSON
                paquete = JsonConvert.DeserializeObject<Paquete>(resultado);


            }
            //se envia el paquete a la vista de edit
            return View(paquete);
        }



        //-------------------------------------------------------------------------
        //POST del edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind] Paquete paquete)
        {
            //se busca en la listad de paquetes
            var responsePackages = await client.GetAsync("/Paquete/listado");

            //se instancia una lista temporar
            List<Paquete> paquetes = null;

            //se espera una respuesta correcta
            if (responsePackages.IsSuccessStatusCode)
            {
                //se guarda el contenido de la repuesta en la lista temporal
                var contenido = await responsePackages.Content.ReadAsStringAsync();
                paquetes = JsonConvert.DeserializeObject<List<Paquete>>(contenido);
            }

            //se bsuca, si hay paquetes con la fecha a reservar
            if (paquetes != null && paquetes.Any(p => p.FechaReserva.Date == paquete.FechaReserva.Date))
            {
                // no permit guardar dos paquetes con la misma fecha
                TempData["Mensaje"] = "Ya existe un paquete con la misma fecha de reserva.";
                return View(paquete);
            }

            //se modficia el paquete en la api
            var subir = client.PostAsJsonAsync("/Paquete/Modificar", paquete);
            await subir;

            //se espera el resultado de la api
            var resultado = subir.Result;
            if (resultado.IsSuccessStatusCode)
            {
                //si el resultado es correcto nos envia a la lista de paquetes
                return RedirectToAction("Index");
            }
            else
            {//respuesta erronea
                TempData["Mensaje"] = "Datos incorrectos";
                return View(paquete);
            }
        }



        //-----------------------------------------------------------------------
        //Get de Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            //se instancia un nuevo objeto
            Paquete paquete = new Paquete();

            //se busca en la api el objeto
            HttpResponseMessage responseMessage = await client.GetAsync($"/Paquete/Buscar/{id}");

            //espera la respuesta de la api
            if (responseMessage.IsSuccessStatusCode)
            {
                //se obtiene los datos de la misma
                var resultado = responseMessage.Content.ReadAsStringAsync().Result;
                paquete = JsonConvert.DeserializeObject<Paquete>(resultado);

            }

            //se envia a la vista de details con los datos obtenidos
            return View(paquete);

        }





        //----------------------------------------------------------------------
        //metodo para obtener el tipo de cambio y enviarlo al correo
        public async Task<decimal> Cambio(decimal montoTotalUSD)
        {
 
            try
            {
                //buscamos los datos en la apiGometa
                HttpResponseMessage response = await client.GetAsync("https://apis.gometa.org/tdc/tdc.json");
                response.EnsureSuccessStatusCode();

                //leemos la respuesta de la api
                string responseBody = await response.Content.ReadAsStringAsync();

               //convertimos el json
                JObject json = JObject.Parse(responseBody);

                //extraemos la venta del json
                decimal exchangeRate = json["venta"].Value<decimal>();

                //pasamos el monto a colones
                decimal montoTotalCRC = montoTotalUSD * exchangeRate;

                //reenviamos el resultado
                return montoTotalCRC;
            }
            //error de lectura de la apis
            catch (HttpRequestException e)
            {
                return 0;
            }
        }



    }//cierre class
}//cierre controller

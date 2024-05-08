namespace AppBibliotecaWebG1.Models
{
    public class TipoCambioAPI
    {
        public HttpClient Iniciar() { 
            
            //Variable para manejar el objeto client
            var client = new HttpClient();

            //Se indica el nombre del dominio donde esta publicada la API
            client.BaseAddress = new Uri("https://apis.gometa.org");

            //Se retorna el objeto cliente
            return client;

        }



    }//Fin de la clase
}//Fin del namespace

namespace AppWebBibliotecaConsumoG1.Models
{
    public class LibrosAPI
    {
        public HttpClient Iniciar() { 
            var client = new HttpClient();

            //Se indica el director base donde esta la API publicada
            client.BaseAddress = new Uri("http://ApiMelber84.somee.com");

            return client;

        }
    }
}

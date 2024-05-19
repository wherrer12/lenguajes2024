namespace AppBibliotecaConsumoG1.Models
{
    public class LibrosAPI
    {

        public HttpClient Iniciar()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("http://apib43383.somee.com/");

            return client;
        }

    }
}

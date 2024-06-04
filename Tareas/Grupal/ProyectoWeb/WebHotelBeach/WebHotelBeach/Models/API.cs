namespace WebHotelBeach.Models
{
    public class API
    {

        //se instancia la api de nuestra base de datos
        public HttpClient Iniciar()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("http://ApiHotelBeach.somee.com");

            return client;
        }



       
    }
}

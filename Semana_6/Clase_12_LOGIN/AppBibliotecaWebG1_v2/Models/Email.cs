using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Net.Http;

namespace AppBibliotecaWebG1.Models
{
    public class Email
    {
        //Metodo encargado de enviar la clave tempral al usuario
        public void Enviar(Usuario usuario)
        {
            try
            {
                //Se crea la instancia del objeto Mail
                MailMessage email = new MailMessage();

                //Se indica el asunto del correo
                email.Subject = "Datos de registro en plataforma web biblioteca CR";

                //Se agrega al email el correo del destinatario
                email.To.Add(new MailAddress(usuario.Email));

                //Se copia el administrador de la cuenta
                //email.Bcc.Add(new MailAddress("Lenguajes2023G2@Outlook.com"));
                email.Bcc.Add(new MailAddress("serverlenguajes2024@​yahoo.com"));

                //Se indica el emisor
                //email.From = new MailAddress("Lenguajes2023G2@Outlook.com");
                email.From = new MailAddress("serverlenguajes2024@​yahoo.com");

                //Se construye el html para el cuerpo del correo
                string html = "Bienvenidos a biblioteca web CR, gracias por formar parte de nuestra plataforma";
                html += "<br>A continuacion detallamos los datos registrados en nuestra plataforma web ";
                html += "<br><b>Email: </b> " + usuario.Email;
                html += "<br><b>Nombre completo: </b> " + usuario.NombreCompleto;
                html += "<br><b>Su contraseña temporal es: </b>" + usuario.Password;
                html += "<br><b>No responda este correo porque fue generado de forma automatica.";
                html += "<br>Plataforma web biblioteca CR</b>";

                //Se indica que el contenido es en html
                email.IsBodyHtml = true;

                //Se indica la prioridad del correo, se recomienda usar normal
                email.Priority = MailPriority.Normal;

                //Se instancia la vista del html para el cuerpo del correo
                AlternateView view = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);

                //Se agrega la view al email
                email.AlternateViews.Add(view);

                //Se configura el protocolo de comunicacion smtp
                SmtpClient smtp = new SmtpClient();

                //Se indica el nombre del servidor smtp a sincronizar la cuenta
                //smtp.Host = "smtp-mail.outlook.com";
                smtp.Host = "smtp.mail.yahoo.com";

                //Se indica el puerto de comunicacion
                smtp.Port = 587;
                //smtp.Port = 465;

                //Se indica que se usara el protocolo de seguridad tipo SSL
                smtp.EnableSsl = true;

                //Se indican las credenciales de la cuenta de correo por default
                smtp.UseDefaultCredentials = false;

                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                //smtp.Credentials = new NetworkCredential("Lenguajes2023G2@Outlook.com", "Ucr2023*");
                smtp.Credentials = new NetworkCredential("serverlenguajes2024@​yahoo.com", "errptill1.");

                //Se envia el correo
                smtp.Send(email);

                //Se liberan los procesos
                email.Dispose();
                smtp.Dispose();

                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }//Cierre class

}//Cierre namespace

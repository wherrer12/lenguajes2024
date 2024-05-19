using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using Examen_1_B43383.Models;

namespace Examen_1_B43383.Models
{
    public class Email
    {

        public void Enviar(Cita citas)
        {
            try
            {
                MailMessage email = new MailMessage();
                email.Subject = "Datos de registro en revisiones tecninas vehiculares";
                email.To.Add(new MailAddress(citas.Email));
                email.Bcc.Add(new MailAddress("Lenguajes2023G2@outlook.com"));
                email.From = new MailAddress("Lenguajes2023G2@outlook.com");

                //Body del correo
                string html = "Bienvenidos a revisiones tecninas vehiculares " +
                    "de nuestra plataforma.<br>A continuación se muestran los datos registrados en el sitio ";
                html += "<br><b>Email: </b> " + citas.Email;
                html += "<br><b>Nombre:</b> " + citas.NombreCompleto;
                html += "<br><b>Su revision es: </b>" + citas.NombreRevision;
                html += "<br><b>Su numer de cita es:" + citas.Id;
                html += "<br><b>Correo creado de forma automatica.";
                html += "<br>Gracias por su preferencia.</b>";

                email.IsBodyHtml = true;
                email.Priority = MailPriority.Normal;
                AlternateView view = AlternateView.CreateAlternateViewFromString(html,Encoding.UTF8, MediaTypeNames.Text.Html);
                email.AlternateViews.Add(view);

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("Lenguajes2023G2@outlook.com", "Ucr2023*");
                smtp.Send(email);

                smtp.Dispose();
                email.Dispose();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }//fin del metodo email

        //Metodo para agregar numero consecutivo al correo
        //public void Consecutivo ()
        //{
            
        //}

    }//cierre class email
}//finnamespace

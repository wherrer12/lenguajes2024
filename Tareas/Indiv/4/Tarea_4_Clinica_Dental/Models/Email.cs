using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;

namespace Tarea_3_Clinica_Dental.Models
{
    public class Email
    {
        //metodo que envia clave temporal al usuario
        public void Enviar(Paciente paciente)
        {
            try
            {

                MailMessage email = new MailMessage();
                email.Subject = "Cita en la Clinica Dental del Pacífico";
                email.To.Add(new MailAddress(paciente.Email));

                //email.Bcc.Add(new MailAddress("Lenguajes2023G2@Outlook.com"));
                //email.From = new MailAddress("Lenguajes2023G2@Outlook.com");
                //email.Bcc.Add(new MailAddress("servidormailucr2024@gmail.com"));
                //email.From = new MailAddress("servidormailucr2024@gmail.com");
                //email.Bcc.Add(new MailAddress("serverlenguajes2024@​yahoo.com"));
                //email.From = new MailAddress("serverlenguajes2024@​yahoo.com");
                email.Bcc.Add(new MailAddress("if4101server@outlook.com"));
                email.From = new MailAddress("if4101server@outlook.com");


                string html = "Bienvenidos a la Clínica Dental del Pacífico";
                html += "<br>Se detalla los datos registrados en la plataforma de registro web";
                html += "<br><b>Cedula: </b>" + paciente.Cedula;
                html += "<br><b>Nombre: </b>" + paciente.Nombre;
                html += "<br><b>Fecha: </b>" + paciente.Fecha;
                html += "<br>";
                html += "<br><b>Gracias por preferirnos";
                html += "<br>";
                html += "<br>No responda a este correo porque fue creado de manera automática</b>";

                email.IsBodyHtml = true;
                email.Priority = MailPriority.Normal;
                AlternateView view = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
                email.AlternateViews.Add(view);
                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp-mail.outlook.com";
                //smtp.Host = "smtp.gmail.com";
                //smtp.Host = "smtp.mail.yahoo.com";

                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;

                //smtp.Credentials = new NetworkCredential("Lenguajes2023G2@Outlook.com", "Ucr2023*");
                //smtp.Credentials = new NetworkCredential("servidormailucr2024@gmail.com", "Querty.1234");
                //smtp.Credentials = new NetworkCredential("serverlenguajes2024@​yahoo.com", "errptill1.");
                smtp.Credentials = new NetworkCredential("if4101server@outlook.com", "ctekttwpjkpqahql");

                smtp.Send(email);
                email.Dispose();
                smtp.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }//Fin clase Email
}//Fin namespace Tarea_4_Clinica_Dental.Models

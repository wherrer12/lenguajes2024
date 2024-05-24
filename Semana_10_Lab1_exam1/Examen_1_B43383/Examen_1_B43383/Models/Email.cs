using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using Examen_1_B43383.Models;
using Microsoft.Build.Framework;

namespace Examen_1_B43383.Models
{
    public class Email
    {
        public void Enviar(Cita citas)
        {
            try
            {
                MailMessage email = new MailMessage();
                email.Subject = "Datos de registro en Revisiones Tecnicas Vehiculares";
                email.To.Add(new MailAddress(citas.Email));
                email.Bcc.Add(new MailAddress("Lenguajes2023G2@outlook.com"));
                email.From = new MailAddress("Lenguajes2023G2@outlook.com");
                //email.Bcc.Add(new MailAddress("if4101server@outlook.com"));
                //email.From = new MailAddress("if4101server@outlook.com");

                //Body del correo
                string html = "Bienvenidos a la plataforma de Revisiones Tecnicas Vehiculares RTV S.A. " +
                    "de Puntarenas.<br>A continuación se muestran los datos registrados en el sitio: ";
                html += "<br><b>Email: </b> " + citas.Email;
                html += "<br><b>Nombre: </b> " + citas.NombreCompleto;
                html += "<br><b>Su revision es de tipo: </b>" + citas.TipoRevision;
                html += "<br><b>Su numero de cita es el numero: " + citas.Id;
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
                //smtp.Credentials = new NetworkCredential("if4101server@outlook.com", "ctekttwpjkpqahql");

                smtp.Send(email);

                smtp.Dispose();
                email.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }//fin del metodo email

        public void EnviarObservacion(Cita citas/*,Diagnostico resultado*/)
        {
            try
            {

                MailMessage email = new MailMessage();
                email.Subject = "Datos de aprobación en Revisiones Tecnicas Vehiculares";
                email.To.Add(new MailAddress(citas.Email));
                email.Bcc.Add(new MailAddress("Lenguajes2023G2@outlook.com"));
                email.From = new MailAddress("Lenguajes2023G2@outlook.com");
                //email.Bcc.Add(new MailAddress("if4101server@outlook.com"));
                //email.From = new MailAddress("if4101server@outlook.com");

                //Body del correo
                string html = "Revisiones Tecnicas Vehiculares RTV le informa " +
                    "que su vehiculo tiene las siguientes observaciones y se adjunta el costo de reparacion: ";
                html += "<br><b>Email: </b> " + citas.Email;
                html += "<br><b>Nombre: </b> " + citas.NombreCompleto;
                html += "<br><b>Su revision es de tipo: </b>" + citas.TipoRevision;
                html += "<br><b>Su numero de cita fue el numero: " + citas.Id;
                html += "<br><b>Observaciones de respuestos cambiados: </b>" + citas.Observacion; // resultado.Observacion;
                html += "<br><b>Acciones de mantenimiento: </b>" + citas.Mantenimiento; // resultado.Mantenimiento;
                html += "<br><b>Costo de reparacion con IVA: </b>" + citas.Total; // resultado.Total;
                html += "<br><b>Correo creado de forma automatica.";
                html += "<br>Gracias por su preferencia.</b>";

                email.IsBodyHtml = true;
                email.Priority = MailPriority.Normal;
                AlternateView view = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
                email.AlternateViews.Add(view);

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("Lenguajes2023G2@outlook.com", "Ucr2023*");
                //smtp.Credentials = new NetworkCredential("if4101server@outlook.com", "ctekttwpjkpqahql");

                smtp.Send(email);

                smtp.Dispose();
                email.Dispose();

            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }//cierre class email
}//finnamespace

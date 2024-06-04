using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.IO;
using System.Diagnostics;

using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Layout.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using iText.Kernel.Font;
using System.Drawing;
using iText.IO.Font.Constants;
using Microsoft.AspNetCore.Mvc;

namespace WebHotelBeach.Models
{
    public class Email
    {

        //Metodo enviar, le da la bienvenida al usuario con un correo
        public void Enviar(Usuario usuario)
        {
            try
            {
                //creacion del remitente y destinatario del email
                MailMessage email = new MailMessage();
                email.Subject = "Datos de registro en plataforma web Hotel Beach";
                email.To.Add(new MailAddress(usuario.Email));
                email.Bcc.Add(new MailAddress("HotelBeach2024@outlook.com"));
                email.From = new MailAddress("HotelBeach2024@outlook.com");


                //creacion del cuerpo del email
                string html = "Bienvenidos a nuestra plataforma digital de Hotel Beach";
                html += "<br> A continuacion detallamos los datos registrados en nuestra pagina web";
                html += "<br><b>Email:</b>" + usuario.Email;
                html += "<br><b>Nombre:</b>" + usuario.NombreCompleto;
                html += "<br><b>No responda a este correo porque fue generado de manera automatica";
                html += "<br>Que tenga un buen dia!</b>";
                email.IsBodyHtml = true;

                //se le da una prioridad al email
                email.Priority = MailPriority.Normal;

                //se crea la vista del email
                AlternateView view = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8,
                    MediaTypeNames.Text.Html);
                email.AlternateViews.Add(view);


                //se ajusta todas las credenciales del SMTP
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("HotelBeach2024@outlook.com", "Ucr2024!");

                //se envia el email
                smtp.Send(email);

                email.Dispose();
                smtp.Dispose();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Variables PATH que otorgan la ruta de los archivos pdf e img
        string path = @"wwwroot\pdf\";
        string imgPath = @"wwwroot\css\img\logo.jpg";

        //Metodo enviar correo con PDF adjunto
        public void EnviarFactura(Cliente comprador, Paquete paquete, decimal cambio)
        {
            try
            {

                //Creacion Email
                MailMessage email = new MailMessage();
                email.Subject = "El Hotel Beach SA adjunta los datos de su reserva";
                email.To.Add(new MailAddress(comprador.Email));
                email.Bcc.Add(new MailAddress("HotelBeach2024@outlook.com"));
                email.From = new MailAddress("HotelBeach2024@outlook.com");

                //Datos string que se muestran en el cuerpo del correo
                string html = "Bienvenidos al Hotel Beach Quepos";
                html += "<br>Se detallan los datos registrados en la plataforma de registro web";
                html += "<br><b>Cedula: </b>" + comprador.Cedula;

                //CREACION PDF
                var exportAsPdfDoc = System.IO.Path.Combine(path, "Reservacion.pdf");//Variable que guarda el nombre del archivo pdf

                using (var writer = new PdfWriter(exportAsPdfDoc)) //Usa la variable exportAsPdfDoc para crear el archivo pdf
                {
                    //Uso de la variable writer para crear el documento pdf
                    using (var pdf = new PdfDocument(writer))
                    {
                        //Variable para la creacion PDF
                        var doc = new Document(pdf, iText.Kernel.Geom.PageSize.LETTER);
                        doc.SetMargins(90, 0, 0, 0);

                        //Creacion de la imagen
                        ImageData imgData = ImageDataFactory.Create(imgPath);
                        var image = new iText.Layout.Element.Image(imgData)

                        //Ajustes de la imagen
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFixedPosition(1, 20, 640)

                        ;//Fin variable image

                        doc.Add(image); // Adicionar imagen

                        //Titulo del documento
                        Paragraph titulo = new Paragraph("Hotel Beach Club S.A.")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(20)
                            .SetBold()
                            .SetMarginBottom(20)

                            ;//Fin variable titulo

                        doc.Add(titulo); // Adicionar titulo

                        //Variables que se pueden cambiar con datos del proyecto y sirven para mencionar informacion de la empresa
                        string empresa = "CADENA DE HOTELES BEACH S.A";
                        string website = "www.hotelesbeach.com";
                        string sucursal = "Quepos, Costa Rica";
                        string direccion = "Calle 45, avenida 14";
                        string telefono = "2664-2525";
                        string vendedor = "Andrea Mora Lizano";
                        string emailEmpresa = "HotelBeach2024@outlook.com";

                        //Colores de los textos
                        PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                        iText.Kernel.Colors.Color colorOrange = new DeviceRgb(255, 80, 0);
                        iText.Kernel.Colors.Color colorBlue = new DeviceRgb(0, 92, 226);
                        iText.Kernel.Colors.Color colorWhite = new DeviceRgb(255, 255, 255);
                        iText.Kernel.Colors.Color colorBlack = new DeviceRgb(0, 0, 0);

                        // Texto del titulo principal
                        Paragraph texto1 = new Paragraph("Datos de reservación")
                            .SetFontSize(32)
                            .SetMargins(0, 0, 0, 0)
                            .SetFontColor(colorOrange)
                            .SetFont(boldFont)
                            .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER)
                            .SetTextAlignment(TextAlignment.CENTER)

                         .SetMarginTop(20)
                         .SetMarginBottom(10)

                        ;//Fin variable texto1

                        doc.Add(texto1); //Adicionar texto1

                        //Telefono o info extra que se quiera adjuntar
                        Paragraph pTel = new Paragraph("Telefono de consultas: " + telefono);
                        pTel.SetFixedPosition(175, 740, 250);
                        pTel.SetPageNumber(1);
                        pTel.SetFont(boldFont);
                        pTel.SetTextAlignment(TextAlignment.CENTER);
                        pTel.SetFontSize(16);
                        pTel.SetFontColor(colorBlue);
                        doc.Add(pTel);

                        //Numero ticket
                        Paragraph ticket = new Paragraph("");
                        ticket.Add("Factura N°:  " + paquete.Id); // Numero de id para que sea el consecutivo de la fcatura

                        ticket.SetFixedPosition(230, 715, 150);
                        ticket.SetPageNumber(1);
                        ticket.SetFont(boldFont);
                        ticket.SetTextAlignment(TextAlignment.CENTER);
                        ticket.SetFontSize(20);
                        ticket.SetFontColor(colorBlue);
                        doc.Add(ticket);

                        //Datos basicos de conctacto de la empresa
                        Paragraph parrafo1 =
                            new Paragraph("FECHA CREACIÓN: " + DateTime.Now + "\n").SetFontSize(12)

                                     .Add("FECHA VIGENCIA: " + paquete.FechaReserva + "\n").SetFontSize(12)
                                     .Add("\n" + empresa + " \n").SetFontSize(12)
                                     .Add("SUCURSAL: " + sucursal + " \n").SetFontSize(12)
                                     .Add("DIRECCIÓN: " + direccion + "\n").SetFontSize(12)
                                     .SetMargins(0, 20, 0, 20)
                                     .Add("VENDEDOR: " + vendedor + "\n")
                                     .Add("\nEstimado cliente " + comprador.Nombre + " en atención a su solicitud, me permito enviarle los detalles correspondiente a los productos y servicios de su reservación");

                        doc.Add(parrafo1).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);

                        Style styleHeader = new Style()
                            .SetBackgroundColor(colorOrange)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(10);

                        Paragraph pDis = new Paragraph("");
                        pDis.Add(new Tab());
                        pDis.AddTabStops(new TabStop(1000, iText.Layout.Properties.TabAlignment.LEFT));
                        pDis.SetMargins(0, 20, 0, 20);
                        pDis.Add("******** Todos los servicios del hotel estan disponibles deacuerdo a su plan de compra. ******** \n");

                        doc.Add(pDis);//Adjuntar parrafo

                        Paragraph pD = new Paragraph("");
                        pD.Add(new Tab());
                        pD.AddTabStops(new TabStop(1000, iText.Layout.Properties.TabAlignment.RIGHT));
                        pD.SetMargins(10, 20, 10, 20);

                        doc.Add(pD);

                        //calculo los espacios


                        Paragraph pn = new Paragraph();
                        pn.SetTextAlignment(TextAlignment.LEFT);
                        pn.SetMargins(0, 20, 0, 20);
                        pn.SetPaddingRight(20);
                        pn.Add("Paquete Comprado: " + paquete.nombrePaquete);
                        doc.Add(pn);

                        Paragraph pc = new Paragraph();
                        pc.SetTextAlignment(TextAlignment.LEFT);
                        pc.SetMargins(0, 20, 0, 20);
                        pc.SetPaddingRight(20);
                        pc.Add("Coste Individual:  $" + paquete.coste);
                        doc.Add(pc);

                        Paragraph perso = new Paragraph();
                        perso.SetTextAlignment(TextAlignment.LEFT);
                        perso.SetMargins(0, 20, 0, 20);
                        perso.SetPaddingRight(20);
                        perso.Add("Cantidad de personas: " + paquete.cantidadPersonas);
                        doc.Add(perso);


                        Paragraph Cnoches = new Paragraph();
                        Cnoches.SetTextAlignment(TextAlignment.LEFT);
                        Cnoches.SetMargins(0, 20, 0, 20);
                        Cnoches.SetPaddingRight(20);
                        Cnoches.Add("Cantidad de noches: " + paquete.cantidadNoches);
                        doc.Add(Cnoches);

                        Paragraph pI = new Paragraph();
                        pI.SetTextAlignment(TextAlignment.LEFT);
                        pI.SetMargins(0, 20, 0, 20);
                        pI.SetPaddingRight(20);
                        pI.Add("Forma de pago: " + paquete.formadePago);
                        doc.Add(pI);

                        if(paquete.numeroCheque != 0)
                        {
                            Paragraph pch = new Paragraph();
                            pch.SetTextAlignment(TextAlignment.LEFT);
                            pch.SetMargins(0, 20, 0, 20);
                            pch.SetPaddingRight(20);
                            pch.Add("Numero de cheque: " + paquete.numeroCheque);
                            doc.Add(pch);
                        }


                        Paragraph pri = new Paragraph();
                        pri.SetTextAlignment(TextAlignment.RIGHT);
                        pri.SetMargins(0, 20, 0, 20);
                        pri.SetPaddingRight(20);
                        pri.Add("Adelanto: $" + paquete.Prima);
                        doc.Add(pri);

                        Paragraph pIm = new Paragraph();
                        pIm.SetTextAlignment(TextAlignment.RIGHT);
                        pIm.SetMargins(0, 20, 0, 20);
                        pIm.SetPaddingRight(20);
                        pIm.Add("Descuento: " + paquete.descuento +"%");
                        doc.Add(pIm);
                        

                        Paragraph pT = new Paragraph("");
                        pT.SetTextAlignment(TextAlignment.RIGHT);
                        pT.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.RIGHT);
                        pT.SetMargins(0, 40, 0, 20);
                        pT.SetFontSize(18);
                        pT.SetWidth(200);
                        pT.SetFont(boldFont);
                        pT.SetBackgroundColor(colorOrange);
                        pT.SetFontColor(colorWhite);
                        pT.SetPaddingRight(10);
                        pT.Add("Total en dolares: $" + paquete.montoTotal+ "\nTotal en colones: "+ $"₡{ cambio: 0.00}");
                        doc.Add(pT);


                        Paragraph pPago = new Paragraph("");
                        pPago.Add("ESTE CORREO ES SU COMPROBANTE DE PAGO, PARA HACER VALIDA SU RESERVA PRESENTELO EN CAJA");

                        pPago.SetFixedPosition(0, 0, 600);
                        pPago.SetPadding(20);
                        pPago.SetFontSize(18);
                        pPago.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                        pPago.SetBackgroundColor(colorBlue);

                        doc.Add(pPago);
                        doc.Close();

                        //Adjuntar PDF al correo
                        email.Attachments.Add(new Attachment("wwwroot\\pdf\\Reservacion.pdf"));

                    }
                }

                //------------------------------ FIN CREACION PDF ------------------------------

                //Datos para la creacion del correo
                email.IsBodyHtml = true;
                email.Priority = MailPriority.Normal;
                AlternateView view = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
                email.AlternateViews.Add(view);
                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp-mail.outlook.com";

                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("HotelBeach2024@outlook.com", "Ucr2024!");

                //Emvio de email y cierre de procesos
                smtp.Send(email);
                email.Dispose();
                smtp.Dispose();

            }

            //Captura de error
            catch (Exception ex)
            {
                throw ex;
            }

        }//Fin metodo enviar email


    }//cierre class

}//cierre namespace

using System.ComponentModel.DataAnnotations;

namespace WebHotelBeach.Models
{
    public class Paquete
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor seleccione un tipo de paquete")]
        public string nombrePaquete { get; set; }

        public decimal coste { 
            get {
                decimal pago = 0;

                if (nombrePaquete != null)
                {
                    if (nombrePaquete.Equals("Todo incluido"))
                    {
                        pago = 450;
                    }
                    else
                    {
                        if (nombrePaquete.Equals("Alimentacion"))
                        {
                            pago = 275;
                        }
                        else
                        {
                            pago = 210;
                        }
                    }

                }

                return pago;


            } }

        [Required(ErrorMessage = "Por favor digite la cantidad de pesonas")]
        public int cantidadPersonas { get; set; }

        [Required(ErrorMessage = "Es necesario que digite la fecha a reservar")]
        public DateTime FechaReserva { get; set; }

        [Required(ErrorMessage = "Por favor digite la cantidad de noches")]
        public int cantidadNoches { get; set; }

        public string formadePago { get; set; }

        public int numeroCheque { get; set; }

        public string EmailUsuario { get; set; }

        public decimal montoTotal
        {
            get
            {

                decimal pago = coste;

                if(pago > 0)
                {

                }
                pago = pago * cantidadPersonas;
                pago = pago * cantidadNoches;
                pago = pago + (pago * 0.13m);//iva
                pago = pago - descuento;
          

                return pago;
            }
        }

        public decimal Prima
        {
            get
            {
                decimal pago = 0;
                if (nombrePaquete != null) {

                    if (nombrePaquete.Equals("Todo incluido"))
                    {
                        pago = 0.45m;
                    }
                    else
                    {
                        if (nombrePaquete.Equals("Alimentacion"))
                        {
                            pago = 0.35m;
                        }
                        else
                        {
                            pago = 0.15m;
                        }
                    }
                    pago = montoTotal * pago;
                }
                

                return pago;

            }
        }

        public int cuotas
        {
            get
            {

                int men = 0;

                if (nombrePaquete != null)
                {

                    if (nombrePaquete.Equals("Todo incluido"))
                    {
                        men = 24;
                    }
                    else
                    {
                        if (nombrePaquete.Equals("Alimentacion"))
                        {
                            men = 18;
                        }
                        else
                        {
                            men = 12;
                        }
                    }
                }
               
                return men;
            }
        }

        public decimal descuento
        {
            get
            {
                decimal descuento = 0;

                if (cantidadNoches != 0)
                {
                    if (cantidadNoches == 3 && cantidadNoches <= 6)
                    {
                        descuento = 0.10m;
                    }
                    else
                    {
                        if (cantidadNoches == 7 && cantidadNoches <= 9)
                        {
                            descuento = 0.15m;
                        }
                        else
                        {
                            if (cantidadNoches == 10 && cantidadNoches <= 12)
                            {
                                descuento = 0.20m;
                            }
                            else
                            {
                                if (cantidadNoches > 13)
                                {
                                    descuento = 25m;

                                }
                            }
                        }
                    }

                }

               
                return descuento;
            }


        }
    
    
    
    }//cierre class
}//cierre namespace

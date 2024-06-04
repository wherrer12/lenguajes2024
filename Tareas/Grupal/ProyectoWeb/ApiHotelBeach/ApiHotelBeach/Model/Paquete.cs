//Libreria a usar
using System.ComponentModel.DataAnnotations;

namespace ApiHotelBeach.Model
{
    //Clase para la tabla Paquete
    public class Paquete
    {
        //Propiedades Id
        [Key]
        public int Id { get; set; }

        //Propiedad nombre del paquete
        public string nombrePaquete { get; set; }

        //Propiedad costo del paquete
        public decimal coste { get; set; }

        //Propiedad cantidad de personas
        public int cantidadPersonas { get; set; }

        //Propiedad fecha de reserva
        public DateTime FechaReserva { get; set; }

        //Propiedad cantidad de noches
        public int cantidadNoches { get; set; }

        //Propiedad forma de pago
        public string formadePago { get; set; }

        //Propiedad numero de tarjeta
        public int numeroCheque { get; set; }

        //Propiedad monto total
        public decimal montoTotal { get; set; }

        //Propiedad prima
        public decimal Prima { get; set; }

        //Propiedad cuotas
        public int cuotas { get; set; }

        //Propiedad descuento
        public decimal descuento { get; set; }

        //Propiedad email del usuario
        public string EmailUsuario { get; set; }
    }//Cierre de la clase
}//Cierre del namespace

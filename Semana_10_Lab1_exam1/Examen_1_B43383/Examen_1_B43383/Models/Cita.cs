using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen_1_B43383.Models
{
    public class Cita
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese su placa")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "Ingrese su cedula")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Ingrese su nombre")]
        [Display(Name = "Nombre")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "Ingrese su correo")]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Seleccione la revision")]
        [Display(Name = "Tipo de revision")]
        public string TipoRevision { get; set; }

        [Required(ErrorMessage = "Ingrese la fecha")]
        [Display(Name = "Fecha")]
        public DateTime FechaHoraRevision { get; set; }

        [Required(ErrorMessage = "Ingrese el estado")]
        public char Estado { get; set; }

        public string Observacion { get; set; }

        public string Mantenimiento { get; set; }

        public double Precio { get; set; }

        [HiddenInput]
        public double Impuesto
        {
            get
            {
                return 0.13;
            }

        }

        [Display(Name = "Precio con IVA")]
        [HiddenInput]
        public double Total
        {
            get
            {
                return Precio + (Precio * Impuesto);
            }
        }

        //public Diagnostico Diagnostico { get; set; }

    }
}

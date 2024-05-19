using System.ComponentModel.DataAnnotations;

namespace Examen_1_B43383.Models
{
    public class Cita
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Placa { get; set; }

        [Required]
        public string Cedula { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string NombreCompleto { get; set; }

        [Required]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Clave")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Revision")]
        public string NombreRevision { get; set;}

        [Required]
        [Display(Name = "Fecha")]
        public DateTime FechaHoraRevision { get; set; }

        [Required]
        public char Estado { get; set; }

        public string Observacion { get; set; }

        public string Mantenimiento { get; set; }

        public string Usuario { get; set; }

        public double Precio { get; set; }

        public double Impuesto { get
            {
                return 0.13;
            }
                
        }

        public double Total { get
            { 
                return Precio * Impuesto;
            }
        }
    }
}

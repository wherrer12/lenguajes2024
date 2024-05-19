using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class Paciente
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese su cedula")]
        [Display(Name = "Cedula")]
        [DataType(DataType.Text)]
        [StringLength(12)]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Ingrese su nombre")]
        [Display(Name = "Nombre")]
        [DataType(DataType.Text)]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese su correo")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Ingrese el procedimiento")]
        [Display(Name = "Procedimiento")]
        public string Procedimiento { get; set; }

        [HiddenInput]
        public double Precio
        {
            get

            {
                if (Procedimiento == "Limpieza")
                {
                    return 15000;
                }
                if (Procedimiento == "Calzas")
                {
                    return 25000;
                }
                if (Procedimiento == "Extracciones")
                {
                    return 10000;
                }
                if (Procedimiento == "Blanqueamiento")
                {
                    return 35000;
                }
                if (Procedimiento == "Ortodoncia")
                {
                    return 350000;
                }
                else
                {
                    return 0;
                }//Fin else
            }//Fin get
        }//Fin metodo precio

        [HiddenInput]
        public double Impuesto
        {
            get
            {
                return Precio * 0.13;
            }//Fin get
        }//Fin metodo impuesto

        [HiddenInput]
        public string Opcion { get; set; }

        [HiddenInput]
        [Display(Name = "Porcentaje")]
        public double PorcentajeAdelanto
        {
            get
            {
                if (Opcion == "Opcion A")
                {
                    return 0.30;
                }
                if (Opcion == "Opcion B")
                {
                    return 0.37;
                }
                if (Opcion == "Opcion C")
                {
                    return 0.43;
                }
                if (Opcion == "Opcion D")
                {
                    return 0.50;
                }
                else
                {
                    return 0;
                }
            }//Fin get

        }//Fin metodo opcion

        [HiddenInput]
        public double Total
        {
            get
            {
                return Precio + Impuesto;
            }//Fin get
        }//Fin metodo total

        [HiddenInput]
        public double Adelanto
        {
            get
            {
                return Total * PorcentajeAdelanto;
            }//Fin get
        }//Fin metodo adelanto
    }
}

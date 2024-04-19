using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tarea_3_Clinica_Dental.Models;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Mvc;

namespace Tarea_3_Clinica_Dental.Models
{
    //[Index(nameof(Fecha), IsUnique = true)] //Para que no se repita la fecha, pero se cae el programa
    public class Paciente
    {
        [Key]
        public int idPaciente { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cedula")]
        [Display(Name = "Cedula")]
        [DataType(DataType.Text)]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Debe ingresar su nombre")]
        [Display(Name = "Nombre Completo")]
        [DataType(DataType.Text)]
        [StringLength(150)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar un correo electronico valido")]
        [Display(Name = "Correo")] // [DisplayName("Correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Seleccione la fecha de la cita")]
        //[Display(Name = "Fecha cita")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:ddd, d/MM/yyyy, hh.mm tt}")]//

        // [IsUnique = true]
        
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe ingresar un procedimiento")]
        [Display(Name = "Procedimiento")]
        public string Procedimiento { get ; set; }

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
                    return 355000;
                }
                else
                {
                    return 0;
                }

            }


        }

        [HiddenInput]
        public double Impuesto
        {
            get
            {
                return Precio * 0.13;

            }

        }

        [HiddenInput]
        public double Total
        {

            get
            {
                return Precio + Impuesto;

            }
        }

        [HiddenInput]
        public double Adelanto
        {

            get
            {
                return (Precio * 0.42) + (Impuesto * 0.42);

            }

        }

    }//Fin clase

}//Fin namespace

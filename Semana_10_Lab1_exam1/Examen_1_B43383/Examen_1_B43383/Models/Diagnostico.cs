//using Microsoft.AspNetCore.Mvc;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Examen_1_B43383.Models
//{
//    public class Diagnostico
//    {
//        [Key]
//        public int IdDiag { get; set; }
//        public string Observacion { get; set; }

//        public string Mantenimiento { get; set; }

//        public double Precio { get; set; }

//        [HiddenInput]
//        public double Impuesto
//        {
//            get
//            {
//                return 0.13;
//            }

//        }

//        [HiddenInput]
//        public double Total
//        {
//            get
//            {
//                return Precio * Impuesto;
//            }
//        }
//        //[ForeignKey("Cita")]
//        //public int Id { get; set; }
//        //public Cita Cita { get; set; }

//    }
//}

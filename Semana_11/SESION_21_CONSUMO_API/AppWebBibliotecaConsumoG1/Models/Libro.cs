using System.ComponentModel.DataAnnotations;

namespace AppWebBibliotecaConsumoG1.Models
{
    public class Libro
    {
        [Key]
        public int ISBN { get; set; }

        [Required(ErrorMessage = "Debe ingresar el titulo")]
        [DataType(DataType.Text)]
        [StringLength(100)]
        public string Titulo { get; set; }


        [Required(ErrorMessage = "Debe ingresar la editorial")]
        [DataType(DataType.Text)]
        [StringLength(150)]
        public string Editorial { get; set; }


        [Required(ErrorMessage = "Debe ingresar el precio venta")]
        [Range(0, Int32.MaxValue)]
        public decimal PrecioVenta { get; set; }


        [Required(ErrorMessage = "Debe seleccionar la foto")]
        [DataType(DataType.Text)]
        public string Foto { get; set; }


        [Required(ErrorMessage = "Debe seleccionar la fecha de registro")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime FechaRegistro { get; set; }

        [Required(ErrorMessage ="Debe seleccionar el estado")]
        public char Estado { get; set; }

        /// <summary>
        /// Fecha programación: 07/0/2024 hora 2.30pm
        /// Método que implementa solamente el verbo  Get para realizar un calculo
        /// </summary>
        //public decimal PrecioDolares { 
        //    get {
        //        return (PrecioVenta / AppBibliotecaWebG2.Controllers.HomeController.tipoCambio.venta);
        //    }
        // }



    } //cierre class
} //cierre namespaces

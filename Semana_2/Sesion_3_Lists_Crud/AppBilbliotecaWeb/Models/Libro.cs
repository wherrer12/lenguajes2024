using System.ComponentModel.DataAnnotations;

namespace AppBilbliotecaWeb.Models

{
    public class Libro
    {
        [Key]
        [Required(ErrorMessage = "Debe ingresar el ISBN")]
        public int ISBN { get; set; }

        [Required(ErrorMessage = "No se permite el titulo en blanco")]
        [DataType(DataType.Text)]
        [StringLength(150)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "No se permite la editorial en blanco")]
        [DataType(DataType.Text)]
        [StringLength(100)]
        public string Editorial { get; set; }

        [Required(ErrorMessage = "Debe ingresar el valor para el precio de venta")]
        [Range(0,9999999.99)]
        public decimal PrecioVenta { get; set; }

        [Required(ErrorMessage = "Seleccione la fecha de publicacion")]
        [DataType(DataType.Date)]
        public DateTime FechaPublicacion { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la foto")]
        [DataType(DataType.Text)]
        public string Foto { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un item")]
        public char Estado { get; set; }

    }//cierre class
}//cierre namespace

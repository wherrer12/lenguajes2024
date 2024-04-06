using System.ComponentModel.DataAnnotations;

namespace AppBibliotecaWebG1.Models
{
    public class Libro
    {
        [Key]
        public int ISBN { get; set; }

        [Required (ErrorMessage = "Debe ingresar el titulo")]
        [DataType(DataType.Text)]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Debe ingresar la editorial, no se per,mite dejarla en blanco")]
        [DataType(DataType.Text)]
        [StringLength(150)]
        public string Editorial { get; set; }

        [Required(ErrorMessage = "Debe ingresar el valor valido")]
        [Range(0,Int32.MaxValue)]
        public decimal PrecioVenta { get; set; }

        [Required(ErrorMessage = "Seleccione la foto")]
        [DataType(DataType.Text)]
        public string Foto { get; set; }

        [Required(ErrorMessage = "Debe ingresar el titulo")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaPublicacion { get; set; }

        [Required(ErrorMessage = "Seleccione un estado")]
        public char Estado { get; set; }


    }
}

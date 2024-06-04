using System.ComponentModel.DataAnnotations;

namespace WebHotelBeach.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el tipo de cedula")]
        public string TipoCedula { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cedula")]
        [DataType(DataType.Text)]
        [StringLength(12)]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre")]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar el telefono")]
        [DataType(DataType.PhoneNumber)]
        public int Telefono { get; set; }

        [Required(ErrorMessage = "Debe ingresar la direccion")]
        [DataType(DataType.Text)]
        [StringLength(150)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe ingresar el correo")]
        [DataType(DataType.EmailAddress)]
        [StringLength(150)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar la clave")]
        [DataType(DataType.Password)]
        [StringLength(150)]
        public string Clave { get; set; }

        public string RolSistema { get; set; }



    }
}

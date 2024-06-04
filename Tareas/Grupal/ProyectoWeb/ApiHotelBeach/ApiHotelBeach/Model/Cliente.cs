//lIBRERIAS A UTILIZAR
using System.ComponentModel.DataAnnotations;

namespace ApiHotelBeach.Model
{
    public class Cliente
    {
        //se crea las propiedades de la clase
        //Propiedad Id
        [Key]
        public int Id { get; set; }

        //Propiedad tipo cedula
        [Required(ErrorMessage = "Debe seleccionar el tipo de cedula")]
        public string TipoCedula { get; set; }

        //Propiedad cedula
        [Required(ErrorMessage = "Debe ingresar la cedula")]
        [DataType(DataType.Text)]
        [StringLength(12)]
        public string Cedula { get; set; }

        //Propiedad nombre
        [Required(ErrorMessage = "Debe ingresar el nombre")]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string Nombre { get; set; }

        //Propiedad apellido
        [Required(ErrorMessage = "Debe ingresar el telefono")]
        [DataType(DataType.PhoneNumber)]
        public int Telefono { get; set; }

        //Propiedad direccion
        [Required(ErrorMessage = "Debe ingresar la direccion")]
        [DataType(DataType.Text)]
        [StringLength(150)]
        public string Direccion { get; set; }

        //Propiedad correo
        [Required(ErrorMessage = "Debe ingresar el correo")]
        [DataType(DataType.EmailAddress)]
        [StringLength(150)]
        public string Email { get; set; }

        //Propiedad clave
        [Required(ErrorMessage = "Debe ingresar la clave")]
        [DataType(DataType.Password)]
        [StringLength(150)]
        public string Clave { get; set; }

        //Propiedad rol
        public string RolSistema { get; set; }

    }//fin de la clase
}//fin del namespace

using System.ComponentModel.DataAnnotations;

namespace Examen_1_B43383.Models
{
    public class Usuario
    {
        [Key]
        [Required(ErrorMessage = "Ingrese su correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingrese su nombre")]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "Ingrese su contraseña, maximo 20 caracteres")]
        [DataType(DataType.Password)]
        [StringLength(20)]
        public string Password { get; set; }

    }
}
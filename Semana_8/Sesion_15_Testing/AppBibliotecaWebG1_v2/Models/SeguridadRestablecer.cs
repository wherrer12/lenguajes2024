using System.ComponentModel.DataAnnotations;

namespace AppBibliotecaWebG1.Models
{
    public class SeguridadRestablecer
    {
        
        public string Email { get; set; }

        [Required (ErrorMessage = "Debe ingresar la clave de seguridad enviada por email")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe ingresar la nueva contraseña")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Importante la confirmacion del password")]
        [DataType(DataType.Password)]
        public string Confirmar { get; set; }

    }
}

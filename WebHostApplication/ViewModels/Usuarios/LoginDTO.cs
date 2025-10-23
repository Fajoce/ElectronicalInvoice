using System.ComponentModel.DataAnnotations;

namespace WebHostApplication.ViewModels.Usuarios
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Ingrese su usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Ingrese su contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

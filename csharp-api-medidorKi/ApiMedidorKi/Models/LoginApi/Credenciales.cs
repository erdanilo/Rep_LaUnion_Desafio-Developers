using System.ComponentModel.DataAnnotations;

namespace ApiMedidorKi.Models.LoginApi
{
    public class Credenciales
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Contrasenia { get; set; }

        [Required]
        public string UsuarioToken { get; set; }

        [Required]
        public string PasswordToken { get; set; }
    }
}
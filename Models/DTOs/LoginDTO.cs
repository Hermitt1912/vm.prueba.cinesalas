using System.ComponentModel.DataAnnotations;

namespace vm.prueba.cinesalas.api.Models.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }
    }

    public class TokenDTO
    {
        public string Token { get; set; }
        public string Username { get; set; }
    }

}

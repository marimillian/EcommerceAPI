using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório")]
        public string Password { get; set; }
    }
}

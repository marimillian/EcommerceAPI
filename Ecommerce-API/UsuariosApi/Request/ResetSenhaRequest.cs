using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Request
{
    public class ResetSenhaRequest
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Compare("Password")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public string Token { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Request;

public class SolicitarResetSenhaRequest
{
    [Required(ErrorMessage = "O campo é obrigatório")]
    [DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido")]
    public string Email { get; set; }
}

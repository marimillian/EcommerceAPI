using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Request;

public class AlteracaoPermissaoRequest
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Role { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.DTO.Usuario;

public class CreateLojistaDto
{
    public readonly string _perfilUsuario = "LOJISTA";

    [Required(ErrorMessage = "O campo é obrigatório")]
    [StringLength(250, ErrorMessage = "O campo deve conter até 250 caracteres")]
    [RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s]+$", 
        ErrorMessage = "O campo deve conter apenas caracteres do alfabeto.")]
    public string Nome { get; set; }

    [Required]
    public string CPF { get; set; }

    [Required(ErrorMessage = "O campo é obrigatório")]
    [DataType(DataType.Date)]
    public DateTime DataNascimento { get; set; }

    [Required(ErrorMessage = "O campo é obrigatório")]
    [DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo é obrigatório")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "O campo é obrigatório")]
    [Compare("Password")]
    public string RePassword { get; set; }

    [Required(ErrorMessage = "O campo é obrigatório")]
    public string CEP { get; set; }
    public string? Logradouro { get; set; }

    [Required(ErrorMessage = "O campo é obrigatório")]
    public int Numero { get; set; }
    public string? Bairro { get; set; }

    [Required(ErrorMessage = "O campo é obrigatório")]
    [StringLength(128, ErrorMessage = "O campo deve conter até 128 caracteres")]
    [RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s0-9]+$", 
        ErrorMessage = "O campo deve conter apenas caracteres alfanuméricos.")]
    public string Complemento { get; set; }
    public string? Localidade { get; set; }
    public string? UF { get; set; }
    
}

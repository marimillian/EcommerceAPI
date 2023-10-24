using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Data.DTOS.CentroDistribuicao;

public class CreateCentroDistribuicaoDto

{
    [Required(ErrorMessage = "O campo deve ser obrigatório")]
    [StringLength(128, ErrorMessage = "O campo deve conter até 128 caracteres")]
    [RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s0-9]+$", ErrorMessage = "O campo deve conter apenas caracteres alfanuméricos.")]
    public string Nome { get; set; }
    public string? Logradouro { get; set; }

    [Required(ErrorMessage = "O campo deve ser obrigatório")]
    public int Numero { get; set; }

    [Required(ErrorMessage = "O campo deve ser obrigatório")]
    [StringLength(128, ErrorMessage = "O campo deve conter até 128 caracteres")]
    [RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s0-9]+$", ErrorMessage = "O campo deve conter apenas caracteres alfanuméricos.")]
    public string Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Localidade { get; set; }
    public string? UF { get; set; }

    [Required]
    public string CEP { get; set; }
    public bool Status { get; set; } = true;
    public DateTime DataCriacao { get; set; } = DateTime.Now;
  
}

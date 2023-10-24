using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Data.DTOS.Produto;

public class UpdateProdutoDto
{

    [Required(ErrorMessage = "O campo deve ser obrigatório")]
    [StringLength(128, ErrorMessage = "O campo deve conter até 128 caracteres")]
    [RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s0-9]+$", ErrorMessage = "O campo deve conter apenas caracteres do alfanumérico.")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O campo deve ser obrigatório")]
    [StringLength(512, ErrorMessage = "O campo deve conter até 512 caracteres")]
    [RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s0-9]+$", ErrorMessage = "O campo deve conter apenas caracteres do alfanumérico.")]
    public string Descricao { get; set; }
    [Required]
    public double Peso { get; set; }
    [Required]
    public double Altura { get; set; }
    [Required]
    public double Largura { get; set; }
    [Required]
    public double Comprimento { get; set; }
    [Required]
    public double Valor { get; set; }
    [Required]
    public int Quantidade { get; set; }
    public bool Status { get; set; }
    public DateTime DataModificacao { get; set; } = DateTime.Now;

    [Required]
    public int CategoriaId { get; set; }
    [Required]
    public int SubcategoriaId { get; set; }
    [Required]
    public int CentroDistribuicaoId { get; set; }
}

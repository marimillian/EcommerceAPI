using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Data.DTOS.Produto;

public class FilterProdutoDto
{
    [RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s]+$", ErrorMessage = "O campo deve conter apenas caracteres do alfabeto.")]
    [MinLength(3, ErrorMessage = "O campo não pode ter menos que 3 caracteres")]
    [StringLength(128, ErrorMessage = "O campo deve conter até 128 caracteres")]
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public double? Peso { get; set; }
    public double? Altura { get; set; }
    public double? Largura { get; set; }
    public double? Comprimento { get; set; }
    public double? Valor { get; set; }
    public int? Quantidade { get; set; }
    public string? CentroDistribuicaoId { get; set; }
    public bool? Status { get; set; }
    public string? CategoriaId { get; set; }
    public string? SubcategoriaId { get; set; }
    public bool? Desc { get; set; }
    public int? Skip { get; set; } = 0;
    public int? Take { get; set; } = 20;

}

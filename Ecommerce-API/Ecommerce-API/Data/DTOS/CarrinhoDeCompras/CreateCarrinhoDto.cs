using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Data.DTOS.CarrinhoDeCompras;

public class CreateCarrinhoDto
{
    [Required]
    public int ProdutoId { get; set; }
    [Required]
    public int Quantidade { get; set; }
    public string? NomeProduto { get; set; }
    public string? ValorProduto { get; set; }


}

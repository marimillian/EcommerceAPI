namespace Ecommerce_API.Data.DTOS.CarrinhoDeCompras;

public class ReadProdutoNoCarrinhoDto
{
    public int ProdutoId { get; set; }
    public string NomeProduto { get; set; }
    public int Quantidade { get; set; }
    public double ValorProduto { get; set; }
    public double ValorTotal { get; set; }

}

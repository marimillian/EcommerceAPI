namespace Ecommerce_API.Models;

public class ProdutoNoCarrinhoModel
{    
    public int CarrinhoId { get; set; }
    public int ProdutoId { get; set; }
    public string NomeProduto { get; set; }
    public int Quantidade { get; set; }
    public double ValorProduto { get; set; }
    public double ValorTotal { get; set; }

    public virtual Produto Produto { get; set; }
    public virtual CarrinhoDeComprasModel CarrinhoDeCompras { get; set; }
}

namespace Ecommerce_API.Models;

public class CarrinhoDeComprasModel
{
    private string _complemento;

    public int Id { get; set; }
    public string? CEP { get; set; }
    public string? Logradouro { get; set; }
    public int? Numero { get; set; }
    public string? Complemento { get { return _complemento; } set { _complemento = value.ToUpper(); } }
    public string? Bairro { get; set; }
    public string? Localidade { get; set; }
    public string? UF { get; set; }

    public virtual ICollection<ProdutoNoCarrinhoModel> ProdutoNoCarrinho { get; set; }

}

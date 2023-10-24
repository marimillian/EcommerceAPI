using Ecommerce_API.Models;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Ecommerce_API.Data.DTOS.CarrinhoDeCompras;

public class ReadCarrinhoDto
{
    public int CarrinhoId { get; set; }
    public string CEP { get; set; }
    public string Logradouro { get; set; }
    public int Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Localidade { get; set; }
    public string UF { get; set; }

    public List<ReadProdutoNoCarrinhoDto> ProdutoNoCarrinhoLista { get; set; }

    public double ValorTotalCarrinho { get; set; }

}

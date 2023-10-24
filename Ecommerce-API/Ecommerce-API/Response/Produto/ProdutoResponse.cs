using Ecommerce_API.Data.DTOS.Links;
using Ecommerce_API.Data.DTOS.Produto;

namespace Ecommerce_API.Response.Produto;

public class ProdutoResponse
{
    public ReadProdutoDto Produto { get; set; }
    public List<LinksDto> Links { get; set; }

    public ProdutoResponse(ReadProdutoDto produto, List<LinksDto> links)
    {
        Produto = produto;
        Links = links;
    }
}

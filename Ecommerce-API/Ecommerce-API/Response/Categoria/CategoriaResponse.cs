namespace Ecommerce_API.Response.Categoria;

using Ecommerce_API.Data.DTOS.Categoria;
using Ecommerce_API.Data.DTOS.Links;

public class CategoriaResponse
{
    public ReadCategoriaDto Categoria { get; set; }
    public List<LinksDto> Links { get; set; }

    public CategoriaResponse(ReadCategoriaDto categoria, List<LinksDto> links)
    {
        Categoria = categoria;
        Links = links;
    }

}

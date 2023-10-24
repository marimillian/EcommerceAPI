using Ecommerce_API.Data.DTOS.Links;
using Ecommerce_API.Data.DTOS.SubCategoria;

namespace Ecommerce_API.Response.Subcategoria
{
    public class SubcategoriaResponse
    {
        public ReadSubCategoriaDto Subcategoria { get; set; }
        public List<LinksDto> Links { get; set; }

        public SubcategoriaResponse(ReadSubCategoriaDto subcategoria, List<LinksDto> links)
        {
            Subcategoria = subcategoria;
            Links = links;
        }
    }
}

using Ecommerce_API.Data.DTOS.CentroDistribuicao;
using Ecommerce_API.Data.DTOS.Links;

namespace Ecommerce_API.Response.CentroDistribuicao
{
    public class CentroDistribuicaoResponse
    {
        public ReadCentroDistribuicaoDto Centro { get; set; }
        public List<LinksDto> Links { get; set; }

        public CentroDistribuicaoResponse(ReadCentroDistribuicaoDto centro, List<LinksDto> links)
        {
            Centro = centro;
            Links = links;
        }
    }
}

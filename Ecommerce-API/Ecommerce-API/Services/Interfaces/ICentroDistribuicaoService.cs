using Ecommerce_API.Data.DTOS.CentroDistribuicao;
using Ecommerce_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Services.Interfaces
{
    public interface ICentroDistribuicaoService
    {
        public Task<CentroDistribuicao> CadastrarCentroDistribuicao([FromBody] CreateCentroDistribuicaoDto centroDto);
        public ReadCentroDistribuicaoDto PesquisarCentroDistribuicaoId(int id);
        public List<ReadCentroDistribuicaoDto> PesquisarCentroDistribuicao(FilterCentroDistribuicaoDto filtro);
        public Task<CentroDistribuicao> EditarCentroDistribuicao(UpdateCentroDistribuicaoDto centroDto, int id);
        public Task<CentroDistribuicao> ApagarCentroDistribuicao(int id);

    }
}

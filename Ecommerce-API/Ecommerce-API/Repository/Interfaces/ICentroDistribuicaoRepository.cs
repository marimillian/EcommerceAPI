using Ecommerce_API.Data.DTOS.CentroDistribuicao;
using Ecommerce_API.Models;

namespace Ecommerce_API.Repository.Interfaces;

public interface ICentroDistribuicaoRepository
{
    public Task<CentroDistribuicao> CadastrarCentroDistribuicao(CentroDistribuicao centro);
    public CentroDistribuicao PesquisarCentroDistribuicaoId(int id);
    public List<CentroDistribuicao> PesquisarCentroDistribuicao(FilterCentroDistribuicaoDto filtro);
    public Task<CentroDistribuicao> EditarCentroDistribuicao(CentroDistribuicao centro);
    public Task<CentroDistribuicao> ApagarCentroDistribuicao(int id);

    public CentroDistribuicao BuscarPorId(int id);
}

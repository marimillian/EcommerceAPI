using Ecommerce_API.Data.DTOS.CarrinhoDeCompras;
using Ecommerce_API.Models;

namespace Ecommerce_API.Services.Interfaces;

public interface ICarrinhoComprasService
{
    public Task<ReadCarrinhoDto> CriarCarrinho(CreateCarrinhoDto carrinhoDto);
    public Task<ReadCarrinhoDto> AdicionarProdutoCarrinho(AddProdutoNoCarrinhoDto carrinhoDto, int carrinhoId, 
        int produtoId);
    public Task<ReadCarrinhoDto> AlterarProdutoNoCarrinho(UpdateProdutoNoCarrinhoDto carrinhoDto, int carrinhoId,
        int produtoId);
    public Task<ReadCarrinhoDto> AdicionarEndereçoNoCarrinho(int id, AddEndereçoNoCarrinhoDto endereçoDto);
    public Task<ReadCarrinhoDto> RemoverProdutoDoCarrinho(int carrinhoId, int produtoId);
    public Task<ReadCarrinhoDto> PesquisarCarrinhoId(int id);
}

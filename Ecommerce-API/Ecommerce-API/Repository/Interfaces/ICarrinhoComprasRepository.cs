using Ecommerce_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_API.Repository.Interfaces
{
    public interface ICarrinhoComprasRepository
    {
        public Task<CarrinhoDeComprasModel> CriarCarrinho(CarrinhoDeComprasModel carrinho);
        public Task<ProdutoNoCarrinhoModel> AdicionarProdutoCarrinho(ProdutoNoCarrinhoModel carrinho);
        public Task<ProdutoNoCarrinhoModel> AlterarProdutoNoCarrinho(ProdutoNoCarrinhoModel carrinho);
        public Task<ProdutoNoCarrinhoModel> RemoverProdutoDoCarrinho(ProdutoNoCarrinhoModel carrinho);
        public Task<CarrinhoDeComprasModel> PesquisarCarrinhoId(int id);
        public CarrinhoDeComprasModel BuscarCarrinhoPorId(int id);
        public ProdutoNoCarrinhoModel BuscarProdutoNoCarrinhoPorId(int id, int carrinhoId);
        public Task<CarrinhoDeComprasModel> AdicionarEndereçoNoCarrinho(CarrinhoDeComprasModel carrinho);
        public bool BuscarProdutoECarrinho(int produtoId, int carrinhoId);
        public List<ProdutoNoCarrinhoModel> ListaProdutosNoCarrinho();



    }
}

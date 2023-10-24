using Ecommerce_API.Data.DTOS.Produto;
using Ecommerce_API.Data.DTOS.SubCategoria;
using Ecommerce_API.Models;

namespace Ecommerce_API.Repository.Interfaces;

public interface IProdutoRepository
{
    public Task<Produto> CadastrarProduto(Produto produto);
    public Produto PesquisarProdutoId(int id);
    public List<Produto> PesquisarProduto(FilterProdutoDto filtro);
    public Task<Produto> EditarProduto(Produto produto);
    public Task<Produto> ApagarProduto(int id);
    public Produto BuscarPorId(int id);
    public List<Produto> ListaProdutos();
    public Task<Produto> AlterarQuantidadeDeEstoque(int id, int quantidade, int estoque);
}

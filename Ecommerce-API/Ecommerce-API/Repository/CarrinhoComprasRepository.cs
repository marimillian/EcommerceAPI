using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS.CarrinhoDeCompras;
using Ecommerce_API.Models;
using Ecommerce_API.Repository.Interfaces;

namespace Ecommerce_API.Repository;

public class CarrinhoComprasRepository : ICarrinhoComprasRepository

{
    private EcommerceContext _context;

    public CarrinhoComprasRepository(EcommerceContext context)
    {
        _context = context;
    }

    public CarrinhoDeComprasModel BuscarCarrinhoPorId(int id)
    {
        return _context.CarrinhoDeCompras.FirstOrDefault(c => c.Id == id);
       
    }
    
    public ProdutoNoCarrinhoModel BuscarProdutoNoCarrinhoPorId(int id, int carrinhoId)
    {
        return _context.ProdutosNoCarrinho.FirstOrDefault(p => p.ProdutoId == id && p.CarrinhoId == carrinhoId);
    }

    public bool BuscarProdutoECarrinho(int produtoId, int carrinhoId)
    {
        return _context.ProdutosNoCarrinho.Any(p => p.ProdutoId == produtoId && p.CarrinhoId == carrinhoId);
    }

    public List<ProdutoNoCarrinhoModel> ListaProdutosNoCarrinho()
    {
        return _context.ProdutosNoCarrinho.ToList();
    }
    
    public async Task<CarrinhoDeComprasModel> CriarCarrinho(CarrinhoDeComprasModel carrinho)
    {
        await _context.CarrinhoDeCompras.AddAsync(carrinho);
        await _context.SaveChangesAsync();
        return carrinho;
    }

    public async Task<ProdutoNoCarrinhoModel> AdicionarProdutoCarrinho(ProdutoNoCarrinhoModel carrinho)
    {
        await _context.ProdutosNoCarrinho.AddAsync(carrinho);
        await _context.SaveChangesAsync();
        return carrinho;
    }

    public async Task<ProdutoNoCarrinhoModel> AlterarProdutoNoCarrinho(ProdutoNoCarrinhoModel carrinho)
    {
        _context.ProdutosNoCarrinho.Update(carrinho);
        await _context.SaveChangesAsync();
        return carrinho;
    }

    public async Task<ProdutoNoCarrinhoModel> RemoverProdutoDoCarrinho (ProdutoNoCarrinhoModel carrinho)
    {
        _context.ProdutosNoCarrinho.Remove(carrinho);
        await _context.SaveChangesAsync();
        
        return carrinho;
    }

    public async Task<CarrinhoDeComprasModel> AdicionarEndereçoNoCarrinho(CarrinhoDeComprasModel carrinho)
    {
        _context.CarrinhoDeCompras.Update(carrinho);
        await _context.SaveChangesAsync();

        return carrinho;
    }

    public async Task<CarrinhoDeComprasModel> PesquisarCarrinhoId(int id)
    {
        var carrinho = BuscarCarrinhoPorId(id);
        
        return carrinho;
    }

    
}

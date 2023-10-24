using Ecommerce_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_API.Data;

public class EcommerceContext : DbContext 
{
    public EcommerceContext(DbContextOptions<EcommerceContext> opts) : base(opts)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ProdutoNoCarrinhoModel>()
            .HasKey(produto => new {  produto.CarrinhoId, produto.ProdutoId });

        builder.Entity<ProdutoNoCarrinhoModel>()
            .HasOne(c => c.CarrinhoDeCompras)
            .WithMany(carrinho => carrinho.ProdutoNoCarrinho)
            .HasForeignKey(produto => produto.CarrinhoId);

        builder.Entity<ProdutoNoCarrinhoModel>()
            .HasOne(c => c.Produto)
            .WithMany(p => p.ProdutoNoCarrinho)
            .HasForeignKey(p => p.ProdutoId);
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<SubCategoria> SubCategorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<CentroDistribuicao> CentrosDistribuicoes { get; set; }
    public DbSet<CarrinhoDeComprasModel> CarrinhoDeCompras { get; set; }
    public DbSet<ProdutoNoCarrinhoModel> ProdutosNoCarrinho { get; set; }

}

using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS.Produto;
using Ecommerce_API.Models;
using Ecommerce_API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_API.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private EcommerceContext _context;
       
        public ProdutoRepository(EcommerceContext context)
        {
            _context = context;
        }

        public Produto BuscarPorId(int id)
        {
            return _context.Produtos.FirstOrDefault(produto => produto.Id == id);
        }

        public List<Produto> ListaProdutos()
        {
            return _context.Produtos.ToList();
        }
        public async Task<Produto> CadastrarProduto(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public List<Produto> PesquisarProduto(FilterProdutoDto filtro)
        {
           
            var sql = "SELECT * FROM `ECOMMERCEAPI`.PRODUTOS WHERE 1=1";

           
            if (!string.IsNullOrEmpty(filtro.Nome))
            {
                sql += $" AND LOCATE ('{filtro.Nome}', NOME)";
            }

            if (filtro.Status == false)
            {
                sql += $" AND STATUS = FALSE";
            }

            if (filtro.Status == true)
            {
                sql += $" AND STATUS = TRUE";
            }

            if (!string.IsNullOrEmpty(filtro.Descricao))
            {
                sql += $" AND LOCATE ('{filtro.Descricao}', DESCRICAO)";
            }

            if (filtro.Altura.HasValue)
            {
                var novo = filtro.Altura.Value.ToString().Replace(',', '.');
                sql += $" AND ALTURA = {novo}";
            }

            if (filtro.Peso.HasValue)
            {
                var novo = filtro.Peso.Value.ToString().Replace(',', '.');
                sql += $" AND PESO = {novo}";
            }

            if (filtro.Largura.HasValue)
            {
                var novo = filtro.Largura.Value.ToString().Replace(',', '.');
                sql += $" AND LARGURA = {novo}";
            }

            if (filtro.Comprimento.HasValue)
            {
                var novo = filtro.Comprimento.Value.ToString().Replace(',', '.');
                sql += $" AND COMPRIMENTO = {novo}";
            }

            if (filtro.Valor.HasValue)
            {
                var novo = filtro.Valor.Value.ToString().Replace(',', '.');
                sql += $" AND VALOR = {novo}";
            }

            if (filtro.Quantidade.HasValue)
            {
                sql += $" AND QUANTIDADE = {filtro.Quantidade.Value}";
            }

            if (!string.IsNullOrEmpty(filtro.CategoriaId))
            {
                var categorias = filtro.CategoriaId.Split(',');
                var categoriaLista = new List<int>();
                foreach (var categoria in categorias)
                {
                    categoriaLista.Add(Int32.Parse(categoria));
                }
                sql += " AND (";

                for (int i = 0; i < categoriaLista.Count; i++)
                {
                    if (i < categoriaLista.Count() -1)
                    {
                        sql += $" CATEGORIAID = {categoriaLista[i]} OR";
                    }
                    else
                    {
                        sql += $" CATEGORIAID = {categoriaLista[i]}";
                    }
                    
                }

                sql += ") ";
               
            }

            if (!string.IsNullOrEmpty(filtro.SubcategoriaId))
            {
                var subcategorias = filtro.SubcategoriaId.Split(',');
                var subcategoriaLista = new List<int>();
                foreach (var subcategoria in subcategorias)
                {
                    subcategoriaLista.Add(Int32.Parse(subcategoria));
                }
                sql += " AND (";

                for (int i = 0; i < subcategoriaLista.Count; i++)
                {
                    if (i < subcategoriaLista.Count() - 1)
                    {
                        sql += $" SUBCATEGORIAID = {subcategoriaLista[i]} OR";
                    }
                    else
                    {
                        sql += $" SUBCATEGORIAID = {subcategoriaLista[i]}";
                    }

                }

                sql += ") ";

            }

            if (!string.IsNullOrEmpty(filtro.CentroDistribuicaoId))
            {
                var centros = filtro.CentroDistribuicaoId.Split(',');
                var centroLista = new List<int>();
                foreach (var centro in centros)
                {
                    centroLista.Add(Int32.Parse(centro));
                }
                sql += " AND (";

                for (int i = 0; i < centroLista.Count; i++)
                {
                    if (i < centroLista.Count() - 1)
                    {
                        sql += $" CENTRODISTRIBUICAOID = {centroLista[i]} OR";
                    }
                    else
                    {
                        sql += $" CENTRODISTRIBUICAOID = {centroLista[i]}";
                    }

                }

                sql += ") ";

            }

            if (filtro.Desc == true)
            {
                sql += $" ORDER BY NOME DESC";
            }

            if (filtro.Desc == false)
            {
                sql += $" ORDER BY NOME";
            }

            if (filtro.Skip.HasValue && filtro.Take.HasValue)
            {
                sql += $" LIMIT {filtro.Skip.Value}, {filtro.Take.Value}";
            }


           var produto = _context.Produtos.FromSqlRaw(sql).ToList();

            return produto;
        }

        public Produto PesquisarProdutoId(int id)
        {
            var produto = BuscarPorId(id); 
            return produto;

        }

        public async Task<Produto> EditarProduto(Produto produto)
        {            
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> ApagarProduto(int id)
        {
            var produto = BuscarPorId(id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> AlterarQuantidadeDeEstoque(int id, int quantidade, int estoque)
        {
            var produto = ListaProdutos().FirstOrDefault(p => p.Id == id);
            int resultado = estoque - quantidade;

            produto.Quantidade = resultado;
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return produto;
        }
    }
}

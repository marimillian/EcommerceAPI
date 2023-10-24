using Ecommerce_API.Data.DTOS.Produto;
using Ecommerce_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Services.Interfaces;

public interface IProdutoService
{
    public Task<Produto> CadastrarProduto(CreateProdutoDto produtoDto);
    public ReadProdutoDto PesquisarProdutoId(int id);
    public List<ReadProdutoDto> PesquisarProduto(FilterProdutoDto filtro);
    public Task<Produto> EditarProduto(UpdateProdutoDto produtoDto, int id);
    public Task<Produto> ApagarProduto(int id);
  
}

using Ecommerce_API.Data.DTOS;
using Ecommerce_API.Data.DTOS.Categoria;
using Ecommerce_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Services.Interfaces;

public interface ICategoriaService

{
    public Task<Categoria> CadastrarCategoria([FromBody] CreateCategoriaDto categoriaDto);
    public List<ReadCategoriaDto> PesquisarCategoria(FilterCategoriaDto filtro);
    public ReadCategoriaDto PesquisarCategoriaId(int id);
    public Task<Categoria> EditarCategoria(int id, [FromBody] UpdateCategoriaDto categoriaDto);
    public Task<Categoria> ApagarCategoria(int id);
}

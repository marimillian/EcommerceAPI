using Ecommerce_API.Data.DTOS.SubCategoria;
using Ecommerce_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Services.Interfaces;

public interface ISubcategoriaService
{
    public Task<SubCategoria> CadastrarSubCategoria(CreateSubCategoriaDto subCategoriaDto);
    public List<ReadSubCategoriaDto> PesquisarSubcategoria(FilterSubCategoriaDto filtro);
    public ReadSubCategoriaDto PesquisarSubCategoriaId(int id);
    public Task<SubCategoria> EditarSubCategoria(UpdateSubCategoriaDto subCategoriaDto, int id);
    public Task<SubCategoria> ApagarSubCategoria(int id);


}

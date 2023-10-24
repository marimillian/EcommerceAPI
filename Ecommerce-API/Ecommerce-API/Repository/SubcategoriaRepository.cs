using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS.SubCategoria;
using Ecommerce_API.Models;
using Ecommerce_API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_API.Repository;

public class SubcategoriaRepository : ISubcategoriaRepository
{
    private EcommerceContext _context;
    //private ILogger<SubcategoriaRepository> _logger;

    public SubcategoriaRepository(EcommerceContext context) //ILogger<SubcategoriaRepository> logger)
    {
        _context = context;
        //_logger = logger;

    }

    public SubCategoria BuscarPorId(int id)
    {
        return _context.SubCategorias.FirstOrDefault(sub => sub.Id == id);
    }
    public async Task<SubCategoria> CadastrarSubcategoria(SubCategoria subcategoria)
    {
        //_logger.LogInformation("Foi requisitada a inserção de uma nova Subcategoria no Banco de Dados");
        await _context.SubCategorias.AddAsync(subcategoria);
        await _context.SaveChangesAsync();
        //_logger.LogInformation($"Foi inserido nova Subcategoria no Banco de Dados. O ID é: {subcategoria.Id}");
        return subcategoria;
    }
      
    public List<SubCategoria> PesquisarSubcategoria(FilterSubCategoriaDto filtro)
    {
       // _logger.LogInformation("Foi requisitada a pesquisa de uma Subcategoria no Banco de Dados");
        var sql = "SELECT * FROM `ECOMMERCEAPI`.SUBCATEGORIAS WHERE 1=1";

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

        var subcategoria = _context.SubCategorias.FromSqlRaw(sql).ToList();
        
        return subcategoria;
    }

    public SubCategoria PesquisarSubcategoriaId(int id)
    {
       // _logger.LogInformation($"Foi requisitada a pesquisa da Subcategoria de ID: {id} no Banco de Dados");
        var subcategoria = BuscarPorId(id);
        return subcategoria;
    }

    public async Task<SubCategoria> EditarSubcategoria(SubCategoria subcategoria)
    {
        //_logger.LogInformation($"Foi requisitada a edição da Subcategoria de ID: {id} no Banco de Dados");
        _context.SubCategorias.Update(subcategoria);
        await _context.SaveChangesAsync();
        //_logger.LogInformation($"Foi atualizada Subcategoria de ID: {id} no Banco de Dados");
        return subcategoria;
    }

    public async Task<SubCategoria> ApagarSubcategoria(int id)
    {
        //_logger.LogInformation($"Requisição para deletar a Subcategoria de ID: {id} no Banco de Dados");
        var subcategoria = BuscarPorId(id);
        _context.Remove(subcategoria);
        await _context.SaveChangesAsync();
        //_logger.LogInformation($"Foi deletada a Subcategoria de ID: {id} no Banco de Dados");
        return subcategoria;
    }

}

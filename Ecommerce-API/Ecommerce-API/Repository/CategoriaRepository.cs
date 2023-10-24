using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS.Categoria;
using Ecommerce_API.Models;
using Ecommerce_API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_API.Repository;

public class CategoriaRepository : ICategoriaRepository
{
    private EcommerceContext _context;
    //private ILogger<CategoriaRepository> _logger;

    public CategoriaRepository(EcommerceContext context) //ILogger<CategoriaRepository> logger
    {
        _context = context;
        //_logger = logger;

    }

    public Categoria BuscarPorId (int id)
    {
        return _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
    }

    public async Task<Categoria> CadastrarCategoria(Categoria categoria)
    {
        //_logger.LogInformation("Foi requisitada a inserção de uma nova Categoria no Banco de Dados");
        await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();
        //_logger.LogInformation($"Foi inserido nova Categoria no Banco de Dados. O ID é: {categoria.Id}");
        return categoria;
    }

    public List<Categoria> PesquisarCategoria(FilterCategoriaDto filtro)
    {
       // _logger.LogInformation("Foi requisitada a pesquisa de uma Categoria no Banco de Dados");

        var sql = "SELECT * FROM `ECOMMERCEAPI`.CATEGORIAS WHERE 1=1";

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

        var categoria = _context.Categorias.FromSqlRaw(sql).ToList();
        
        return categoria;
    }

    public Categoria PesquisarCategoriaId(int id)
    {
       // _logger.LogInformation($"Foi requisitada a pesquisa da Categoria de ID: {id} no Banco de Dados");
        var categoria = BuscarPorId(id);
        return categoria;
    }

    public async Task<Categoria> EditarCategoria(Categoria categoria)
    {
        //_logger.LogInformation($"Foi requisitada a edição da Categoria de ID: {id} no Banco de Dados");
        _context.Update(categoria);
        await _context.SaveChangesAsync();
        //_logger.LogInformation($"Foi atualizada a Categoria de ID: {id} no Banco de Dados");
        return categoria;
    }

    public async Task<Categoria> ApagarCategoria(int id)
    {
        //_logger.LogInformation($"Requisição para deletar a Categoria de ID: {id} no Banco de Dados");
        var categoria = BuscarPorId(id);
        _context.Remove(categoria);
        await _context.SaveChangesAsync();
       // _logger.LogInformation($"Foi deletada a Categoria de ID: {id} no Banco de Dados");
        return categoria;
    }
}

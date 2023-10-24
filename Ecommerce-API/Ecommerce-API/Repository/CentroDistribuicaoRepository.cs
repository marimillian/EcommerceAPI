using Dapper;
using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS.CentroDistribuicao;
using Ecommerce_API.Models;
using Ecommerce_API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
namespace Ecommerce_API.Repository;

public class CentroDistribuicaoRepository : ICentroDistribuicaoRepository
{
    private EcommerceContext _context;
 
    private IConfiguration _configuration;

    public CentroDistribuicaoRepository(EcommerceContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;

    }

    public CentroDistribuicao BuscarPorId(int id)
    {
        return _context.CentrosDistribuicoes.FirstOrDefault(centro => centro.Id == id);
    }

   public async Task<CentroDistribuicao> CadastrarCentroDistribuicao(CentroDistribuicao centro)
    {
        await _context.CentrosDistribuicoes.AddAsync(centro);
        await _context.SaveChangesAsync();
        return centro;
    }

    public List<CentroDistribuicao> PesquisarCentroDistribuicao(FilterCentroDistribuicaoDto filtro)
    {
       
        var sql = "SELECT * FROM `ECOMMERCEAPI`.CENTROSDISTRIBUICOES WHERE 1=1";
         
        using var connection = new MySqlConnection(_configuration.GetConnectionString("EcommerceAPIConnection"));
        

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

            if (!string.IsNullOrEmpty(filtro.CEP))
            {
                sql += $" AND LOCATE ('{filtro.CEP}', CEP)";
            }

            if (!string.IsNullOrEmpty(filtro.Logradouro))
            {
                sql += $" AND LOCATE ('{filtro.Logradouro}', LOGRADOURO)";
            }

            if (filtro.Numero.HasValue)
            {
                sql += $" AND NUMERO = {filtro.Numero.Value}";
            }

            if (!string.IsNullOrEmpty(filtro.Complemento))
            {
                sql += $" AND LOCATE ('{filtro.Complemento}', COMPLEMENTO)";
            }

            if (!string.IsNullOrEmpty(filtro.Bairro))
            {
                sql += $" AND LOCATE ('{filtro.Bairro}', Bairro)";
            }

            if (!string.IsNullOrEmpty(filtro.Localidade))
            {
                sql += $" AND LOCATE ('{filtro.Localidade}', LOCALIDADE)";
            }

            if (!string.IsNullOrEmpty(filtro.UF))
            {
                sql += $" AND LOCATE ('{filtro.UF}', UF)";
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

          var centros = connection.Query<CentroDistribuicao>(sql).ToList();
           
        return centros;
    }

    public CentroDistribuicao PesquisarCentroDistribuicaoId(int id)
    {
        var centro = BuscarPorId(id);
        return centro;
    }

    public async Task<CentroDistribuicao> EditarCentroDistribuicao(CentroDistribuicao centro)
    {
        _context.CentrosDistribuicoes.Update(centro);
        await _context.SaveChangesAsync();
        return centro;
    }

    public async Task<CentroDistribuicao> ApagarCentroDistribuicao(int id)
    {
        var centro = BuscarPorId(id);
        _context.Remove(centro);
        await _context.SaveChangesAsync();
        return centro;
    }

}

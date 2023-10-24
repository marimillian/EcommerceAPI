using AutoMapper;
using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS.CentroDistribuicao;
using Ecommerce_API.Models;
using Ecommerce_API.Repository.Interfaces;
using Ecommerce_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Ecommerce_API.Services;

public class CentroDistribuicaoService : ICentroDistribuicaoService
{
    EcommerceContext _context;
    IMapper _mapper;
    ICentroDistribuicaoRepository _repository;

    public CentroDistribuicaoService(EcommerceContext context, IMapper mapper, ICentroDistribuicaoRepository repository)
    {
        _context = context;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Endereco> ConsultarViaCep(string cep)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");
            var jsonObject = JsonSerializer.Deserialize<Endereco>(response);
            return jsonObject;
        }

        catch
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.CentroDistribuicao.ErrorMessage.cepIncorreto);
        }

    }

    private List<CentroDistribuicao> ListaCentros()
    {
        return _context.CentrosDistribuicoes.ToList();
    }

    public async Task<CentroDistribuicao> CadastrarCentroDistribuicao([FromBody] CreateCentroDistribuicaoDto centroDto)
    {
        var viaCep = await ConsultarViaCep(centroDto.CEP);
        _mapper.Map(viaCep, centroDto);
        var centro = _mapper.Map<CentroDistribuicao>(centroDto);
        VerificacaoDosDados(centroDto.Nome, centroDto.Numero, centroDto.Complemento, centroDto.CEP);
        await _repository.CadastrarCentroDistribuicao(centro);
        return centro;
    }

    public List<ReadCentroDistribuicaoDto> PesquisarCentroDistribuicao(FilterCentroDistribuicaoDto filtro)
    {
        var centro = _repository.PesquisarCentroDistribuicao(filtro);
        if (centro == null) return null;
        var centroDto = new List<ReadCentroDistribuicaoDto>();
        _mapper.Map(centro, centroDto);
        return centroDto;
    }

    public ReadCentroDistribuicaoDto PesquisarCentroDistribuicaoId(int id)
    {
        var centro = _repository.PesquisarCentroDistribuicaoId(id);
        var centroDto = _mapper.Map<ReadCentroDistribuicaoDto>(centro);
        return centroDto;
    }
    public async Task<CentroDistribuicao> EditarCentroDistribuicao(UpdateCentroDistribuicaoDto centroDto, int id)
    {
        var viaCep = await ConsultarViaCep(centroDto.CEP);
        _mapper.Map(viaCep, centroDto);
        var centro = _repository.BuscarPorId(id);
        VerificacaoDosDados(centroDto.Nome, centroDto.Numero, centroDto.Complemento, centroDto.CEP, centroDto.Status, id);
        _mapper.Map(centroDto, centro);
        await _repository.EditarCentroDistribuicao(centro);
        return centro;
    }

    public async Task<CentroDistribuicao> ApagarCentroDistribuicao(int id)
    {
        var centro = await _repository.ApagarCentroDistribuicao(id);
        return centro;
    }


    private void VerificacaoDosDados(string nome, int numero, string complemento, string cep, bool? status = null, int? id = null)
    {
        if (_context.Categorias.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.CentroDistribuicao.ErrorMessage.categoriaExistente);
        }

        if (_context.SubCategorias.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.CentroDistribuicao.ErrorMessage.subcategoriaExistente);
        }

        if (_context.Produtos.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.CentroDistribuicao.ErrorMessage.produtoExistente);
        }

        if (ListaCentros().Any(c => c.Nome.ToUpper() == nome.ToUpper() && c.Id != id))
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.CentroDistribuicao.ErrorMessage.centroExistente);
        }

        if (ListaCentros().Any(c => c.CEP == cep && c.Numero == numero
        && c.Complemento.ToUpper() == complemento.ToUpper() && c.Id != id))
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.CentroDistribuicao.ErrorMessage.enderecoExistente);
        }

        if (_context.Produtos.Where(c => c.CentroDistribuicaoId == id).Count() > 0 && status == false)
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.CentroDistribuicao.ErrorMessage.produtoAtivo);
        }

        if (cep.Contains('-'))
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.CentroDistribuicao.ErrorMessage.cepHifen);
        }
    }

}

using AutoMapper;
using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS.SubCategoria;
using Ecommerce_API.Models;
using Ecommerce_API.Repository.Interfaces;
using Ecommerce_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Services
{
    public class SubcategoriaService : ISubcategoriaService
    {
        private EcommerceContext _context;
        private IMapper _mapper;
        private ISubcategoriaRepository _repository;
        private ILogger<SubcategoriaService> _logger;
        public SubcategoriaService(EcommerceContext context, IMapper mapper, ISubcategoriaRepository repository) //ILogger<SubcategoriaService> logger)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;
            //_logger = logger;
        }
        private List<SubCategoria> ListaSubcategoria()
        {
            return _context.SubCategorias.ToList();
        }

        public async Task<SubCategoria> CadastrarSubCategoria(CreateSubCategoriaDto subCategoriaDto)
        {
            //_logger.LogInformation("Foi requisitada as regras de negócio para cadastrar uma Subcategoria");
            SubCategoria subCategoria = _mapper.Map<SubCategoria>(subCategoriaDto);
            VerificacaoDosDados(subCategoriaDto.Nome, subCategoria.CategoriaId);
            await _repository.CadastrarSubcategoria(subCategoria);
            return subCategoria;
        }

        public List<ReadSubCategoriaDto> PesquisarSubcategoria(FilterSubCategoriaDto filtro)
        {
            //_logger.LogInformation("Foi requisitada as regras de negócio para pesquisar uma Subcategoria");
            var subcategoria = _repository.PesquisarSubcategoria(filtro);
            if (subcategoria == null) return null; 
            var subcategoriaDto = new List<ReadSubCategoriaDto>();
            _mapper.Map(subcategoria, subcategoriaDto);

             return subcategoriaDto;

        }

        public ReadSubCategoriaDto PesquisarSubCategoriaId(int id)
        {
           // _logger.LogInformation($"Foi requisitada as regras de negócio para pesquisar uma Subcategoria de ID: {id}");
            var subCategoria = _repository.PesquisarSubcategoriaId(id);
            if (subCategoria == null) return null;
            var subCategoriaDto = _mapper.Map<ReadSubCategoriaDto>(subCategoria);
            return subCategoriaDto;
        }

        public async Task<SubCategoria> EditarSubCategoria([FromBody] UpdateSubCategoriaDto subCategoriaDto, int id)
        {
            //_logger.LogInformation($"Foi requisitada as regras de negócio para editar uma Subcategoria de ID: {id}");
            var subcategoria = _repository.BuscarPorId(id);
            if (subcategoria == null) return null;
            VerificacaoDosDados(subCategoriaDto.Nome, subcategoria.CategoriaId, subcategoria.Status, id);
            _mapper.Map(subCategoriaDto, subcategoria);
            subcategoria = await _repository.EditarSubcategoria(subcategoria);

            return subcategoria;
        }

       
        public async Task<SubCategoria> ApagarSubCategoria(int id)
        {
           // _logger.LogInformation($"Foi requisitada as regras de negócio para editar uma Subcategoria de ID: {id}");
            var subCategoria = await _repository.ApagarSubcategoria(id);
            if (subCategoria == null) return null;
            return subCategoria;

        }

        private void VerificacaoDosDados(string nome, int categoriaId, bool? status = null, int? id = null)
        {
            if (ListaSubcategoria().Any(c => c.Nome.ToUpper() == nome.ToUpper() && c.Id != id))
            {
                throw new NameExceptions(Ecommerce_API.ConstMessages.Subcategoria.ErrorMessage.subcategoriaExistente);
            }

            if (_context.Categorias.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
            {
                throw new NameExceptions(Ecommerce_API.ConstMessages.Subcategoria.ErrorMessage.categoriaExistente);
            }

            if (_context.Categorias.Any(c => c.Id == categoriaId && c.Status == false))
            {
                throw new NameExceptions(Ecommerce_API.ConstMessages.Subcategoria.ErrorMessage.categoriaInativa);
            }
            if (_context.Produtos.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
            {
                throw new NameExceptions(Ecommerce_API.ConstMessages.Subcategoria.ErrorMessage.produtoExistente);
            }

            if (_context.CentrosDistribuicoes.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
            {
                throw new NameExceptions(Ecommerce_API.ConstMessages.Subcategoria.ErrorMessage.centroExistente);
            }

            if (_context.Produtos.Where(c => c.SubcategoriaId == id).Count() > 0 && status == false)
            {
                throw new NameExceptions(Ecommerce_API.ConstMessages.Subcategoria.ErrorMessage.produtoAtivo);
            }

            
        }

    }

}

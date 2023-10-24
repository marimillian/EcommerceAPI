using AutoMapper;
using Ecommerce_API.Data.DTOS;
using Ecommerce_API;
using Ecommerce_API.Profiles;
using Ecommerce_API.Repository;
using Ecommerce_API.Services;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPITest.Categoria
{
    public class CategoriaServiceTest
    {
        private readonly CategoriaService _service;
        private readonly CategoriaRepository _repository;
        private readonly IMapper _mapper;

        public CategoriaServiceTest()
        {
            var config = new MapperConfiguration(options => 
            { options.AddProfile(new CategoriaProfile()); });
            _mapper = config.CreateMapper();

            var dbInMemory = new DBInMemory();
            var context = dbInMemory.GetContext();

            _repository = new CategoriaRepository(context);
            _service = new CategoriaService(context, _mapper, _repository);
        }

        [Fact]

        public void Nome_Repetido()
        {
            //Arrange
            var categoriaDto = new CreateCategoriaDto();
            categoriaDto.Nome = "COMIDA";

            //Assert
            Assert.ThrowsAsync<NameExceptions>(() => _service.CadastrarCategoria(categoriaDto));
        }


        [Fact]
        public void Nome_Com_CaracterEspecial()
        {
            //Arrange
            var verificar = new RegularExpressionAttribute
                ("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s]+$");
            var nome = "Jose@";

            //Act
            var result = verificar.IsValid(nome);

            //Assert
            Assert.False(result);

        }

        [Fact]

        public void Nome_Com_Menos_De_128_Caracteres()
        {
            //Arrange
            var verificar = new StringLengthAttribute(128);
            var nome = "COMIDA";

            //Act
            var result = verificar.IsValid(nome);

            //Assert
            Assert.True(result);
        }

        [Fact]

        public void Nome_Com_Mais_De_128_Caracteres()
        {
            //Arrange
            var verificar = new StringLengthAttribute(128);
            var nome = "COMIDAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";

            //Act
            var result = verificar.IsValid(nome);
            
            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Nome_Obrigatório_Vazio()
        {
            //Arrange
            var verificar = new RequiredAttribute();
            var nome = "";

            //Act
            var result = verificar.IsValid(nome);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Nome_Obrigatorio_Preenchido()
        {
            //Arrange
            var verificar = new RequiredAttribute();
            var nome = "COMIDA";

            //Act
            var result = verificar.IsValid(nome);

            //Assert
            Assert.True(result);
        }

        [Fact]

        public async Task Cadastrar_Categoria_Status_Ativo()
        {
            //Arrange
            var categoriaDto = new CreateCategoriaDto();
            categoriaDto.Nome = "CategoriaTeste";

            //Act
            var cadastrar = await _service.CadastrarCategoria(categoriaDto);

            //Assert
            Assert.True(cadastrar.Status);
        }

        [Fact]
        public async Task Cadastrar_Categoria_DataCriaçao()
        {
            //Arrange
            var categoriaDto = new CreateCategoriaDto();
            categoriaDto.Nome = "CategoriaTesteDois";

            //Act
            var cadastrar = await _service.CadastrarCategoria(categoriaDto);

            //Assert
            Assert.Equal(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), 
                cadastrar.DataCriacao.ToString("dd/MM/yyyy HH:mm"));
        }


    }
}

using AutoMapper;
using Ecommerce_API;
using Ecommerce_API.Profiles;
using Ecommerce_API.Repository;
using Ecommerce_API.Services;
using System.ComponentModel.DataAnnotations;
using Ecommerce_API.Data.DTOS.SubCategoria;

namespace EcommerceAPITest.Subcategoria
{
    public class SubcategoriaServiceTest
    {
        private readonly IMapper _mapper;
        private readonly SubcategoriaRepository _repository;
        private readonly SubcategoriaService _service;
        public SubcategoriaServiceTest()
        {
            var config = new MapperConfiguration(options =>
            { options.AddProfile(new SubCategoriaProfile()); });
            _mapper = config.CreateMapper();

            var dbInMemory = new DBInMemory();
            var context = dbInMemory.GetContext();

            _repository = new SubcategoriaRepository(context);
            _service = new SubcategoriaService(context, _mapper, _repository);
        }

        [Fact]

        public void Nome_Repetido()
        {
            //Arrange
            var subcategoriaDto = new CreateSubCategoriaDto();
            subcategoriaDto.Nome = "CARNE";
            subcategoriaDto.CategoriaId = 1;

            //Assert
            Assert.ThrowsAsync<NameExceptions>(() => _service.CadastrarSubCategoria(subcategoriaDto));
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
            var nome = "BEBIDA";
            
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
            var nome = "BEBIDAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
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
            var nome = "BEBIDA";

            //Act
            var result = verificar.IsValid(nome);

            //Assert
            Assert.True(result);
        }

        [Fact]

        public async Task Cadastrar_Subcategoria_Status_Ativo()
        {
            //Arrange
            var subcategoriaDto = new CreateSubCategoriaDto();
            subcategoriaDto.Nome = "CategoriaTeste";
            subcategoriaDto.CategoriaId = 1;

            //Act
            var cadastrar = await _service.CadastrarSubCategoria(subcategoriaDto);

            //Assert
            Assert.True(cadastrar.Status);
        }

        [Fact]
        public async Task Cadastrar_Subcategoria_DataCriaçao()
        {
            //Arrange
            var subcategoriaDto = new CreateSubCategoriaDto();
            subcategoriaDto.Nome = "SubcategoriaTesteDois";
            subcategoriaDto.CategoriaId = 1;

            //Act
            var cadastrar = await _service.CadastrarSubCategoria(subcategoriaDto);

            //Assert
            Assert.Equal(DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                cadastrar.DataCriacao.ToString("dd/MM/yyyy HH:mm"));
        }

        [Fact]

        public async Task Cadastrar_Subcategoria_Em_Categoria_Inativa()
        {
            //Arrange
            var subcategoriaDto = new CreateSubCategoriaDto();
            subcategoriaDto.Nome = "SubCategoriaTesteTres";
            subcategoriaDto.CategoriaId = 3;

            //Assert
            Assert.ThrowsAsync<NameExceptions>(() => _service.CadastrarSubCategoria(subcategoriaDto));
        }
    }
}

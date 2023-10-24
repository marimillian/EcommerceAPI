using Ecommerce_API.Repository;

namespace EcommerceAPITest.Subcategoria
{
    public class SubcategoriaRepositoryTest
    {
        private readonly SubcategoriaRepository _repository;

        public SubcategoriaRepositoryTest()
        {
            var dbInMemory = new DBInMemory();
            var context = dbInMemory.GetContext();

            _repository = new SubcategoriaRepository(context);
        }

        [Fact]

        public async Task Cadastrar_Subcategoria()
        {
            //Arrange
            var subcategoria = new Ecommerce_API.Models.SubCategoria();
            subcategoria.Nome = "SubcategoriaTesteUm";
            subcategoria.CategoriaId = 1;

            //Act
            var cadastrar = await _repository.CadastrarSubcategoria(subcategoria);

            //Assert
            Assert.Equal(2, cadastrar.Id);

        }

        [Fact]

        public async Task Pesquisar_Por_ID()
        {
            //Act 
            var subcategorias = _repository.PesquisarSubcategoriaId(1);

            //Assert
            Assert.NotNull(subcategorias);
        }
    }
}

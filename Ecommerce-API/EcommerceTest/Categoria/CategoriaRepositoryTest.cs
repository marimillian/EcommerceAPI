using Ecommerce_API.Repository;



namespace EcommerceAPITest.Categoria;

public class CategoriaRepositoryTest
{
    private readonly CategoriaRepository _repository;
    

    public CategoriaRepositoryTest()
    {
        var dbInMemory = new DBInMemory();
        var context = dbInMemory.GetContext();

        _repository = new CategoriaRepository(context);
    }

    [Fact]

    public async Task Cadastrar_Categoria()
    {
        //Arrange
        var categoria = new Ecommerce_API.Models.Categoria();
        categoria.Nome = "CategoriaTesteUm";

        //Act
        var cadastrar = await _repository.CadastrarCategoria(categoria);

        //Assert
        Assert.Equal(4, cadastrar.Id); //O Id 4 é o próximo número de ID a ser cadastrado no banco

    }

    [Fact]

    public async Task Pesquisar_Por_ID()
    {
        //Act
        var categorias = _repository.PesquisarCategoriaId(1);

        //Assert
        Assert.NotNull(categorias);
    }
}

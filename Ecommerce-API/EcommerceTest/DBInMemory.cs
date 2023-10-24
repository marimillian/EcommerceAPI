using Ecommerce_API.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPITest;

public class DBInMemory
{
    private EcommerceContext _context;

    public DBInMemory()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<EcommerceContext>()
            .UseSqlite(connection)
            .EnableSensitiveDataLogging()
            .Options;
        _context = new EcommerceContext(options);
        InsertFakeData();
    }

    public EcommerceContext GetContext() => _context;

    private void InsertFakeData()
    {
        if (_context.Database.EnsureCreated())
        {
            _context.Categorias.Add(
                new Ecommerce_API.Models.Categoria() 
                { Id = 1, Nome = "COMIDA", Status = true, DataCriacao = DateTime.Now});
            _context.Categorias.Add(
                new Ecommerce_API.Models.Categoria()
                { Id = 3, Nome = "LIVROS", Status = false, DataCriacao = DateTime.Now });
            _context.SubCategorias.Add(
                new Ecommerce_API.Models.SubCategoria()
                { Id = 1, CategoriaId = 1, Nome = "CARNE", Status = true, DataCriacao= DateTime.Now});

            _context.SaveChanges();
        }
    }
}

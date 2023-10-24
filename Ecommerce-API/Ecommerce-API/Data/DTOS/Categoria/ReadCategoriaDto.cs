using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Data.DTOS.Categoria
{
    public class ReadCategoriaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
        public string DataDeCriacao { get; set; }
        public string DataDeModificacao { get; set; }
        public int QtdSubcategorias { get; set; }

       
    }
}

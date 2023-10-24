using Ecommerce_API.Data.DTOS.Categoria;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Data.DTOS.SubCategoria
{
    public class ReadSubCategoriaDto
    {
        public int Id { get; set; } 
        public string Nome { get; set; }
        public bool Status { get; set; }
        public string DataDeCriacao { get; set; }
        public string DataDeModificacao { get; set; }
        public string CategoriaNome { get; set; }
        public int QtdProdutos { get; set; }

        
    }
}

using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Data.DTOS.Produto
{
    public class ReadProdutoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
        public double Valor { get; set; }
        public int Quantidade { get; set; }
        public string CentroDistribuicao { get; set; }
        public bool Status { get; set; }
        public string DataDeCriacao { get; set; }
        public string DataDeModificacao { get; set; }
        public string CategoriaNome { get; set; }
        public string SubcategoriaNome { get; set; }
    }
}

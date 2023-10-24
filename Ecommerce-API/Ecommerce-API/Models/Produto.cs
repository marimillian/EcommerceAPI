using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;


namespace Ecommerce_API.Models;

public class Produto
{
    private string _nome;
    private string _descricao;

    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo deve ser obrigatório")]
    [StringLength(128, ErrorMessage = "O campo deve conter até 128 caracteres")]
    [RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s0-9]+$", ErrorMessage = "O campo deve conter apenas caracteres alfanumérico.")]
    public string Nome { get { return _nome; } set { _nome = value.ToUpper(); } 
}
    [Required(ErrorMessage = "O campo deve ser obrigatório")]
    [StringLength(512, ErrorMessage = "O campo deve conter até 512 caracteres")]
    [RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s0-9]+$", ErrorMessage = "O campo deve conter apenas caracteres do alfanumérico.")]
    public string Descricao { get { return _descricao; } set { _descricao = value.ToUpper(); } }

    [Required]
    public double Peso { get; set; }

    [Required]
    public double Altura { get; set; }

    [Required]
    public double Largura { get; set; }

    [Required]
    public double Comprimento { get; set; }

    [Required]
    public double Valor { get; set; }

    [Required]
    public int Quantidade { get; set; }

    public bool Status { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataModificacao { get; set; }

    [Required]
    public int CategoriaId { get; set; }
    [Required]
    public int SubcategoriaId { get; set; }

    [Required]
    public int CentroDistribuicaoId { get; set; }

    public virtual Categoria Categoria { get; set; }
    public virtual SubCategoria SubCategoria { get; set; }
    public virtual CentroDistribuicao CentroDistribuicao { get; set; }
    public virtual ICollection<ProdutoNoCarrinhoModel> ProdutoNoCarrinho { get; set; }

}

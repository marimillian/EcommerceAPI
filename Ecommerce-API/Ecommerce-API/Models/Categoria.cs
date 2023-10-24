using Ecommerce_API.Data.DTOS;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Models;

public class Categoria
{
    private string _nome;

    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O campo deve ser obrigatório")]
    [StringLength(128, ErrorMessage = "O campo deve conter até 128 caracteres")]
    [RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s]+$", ErrorMessage = "O campo deve conter apenas caracteres do alfabeto.")]
    public string Nome { get { return _nome; } set { _nome = value.ToUpper(); } } 
    public bool Status { get; set; } 
    public DateTime DataCriacao { get; set; }
    public DateTime DataModificacao { get; set; }
    public virtual ICollection<SubCategoria> SubCategorias { get; set;} 
    public virtual ICollection<Produto> Produtos { get; set; }


}

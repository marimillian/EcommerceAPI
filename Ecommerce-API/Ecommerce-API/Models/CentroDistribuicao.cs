using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Models;

public class CentroDistribuicao
{
    private string _nome;
    private string _complemento;
    
    [Required]
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo deve ser obrigatório")]
    [StringLength(128, ErrorMessage = "O campo deve conter até 128 caracteres")]
    [RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s0-9]+$", ErrorMessage = "O campo deve conter apenas caracteres alfanuméricos.")]
    public string Nome { get { return _nome; } set { _nome = value.ToUpper(); } }
    public string Logradouro { get; set; }

    [Required]
    public int Numero { get; set; }

    [Required(ErrorMessage = "O campo deve ser obrigatório")]
    [StringLength(128, ErrorMessage = "O campo deve conter até 128 caracteres")]
    [RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s0-9]+$", ErrorMessage = "O campo deve conter apenas caracteres alfanuméricos.")]
    public string Complemento { get { return _complemento; } set { _complemento = value.ToUpper(); } }

    public string Bairro { get; set; }

    public string Localidade { get; set; }

    public string UF { get; set; }

    [Required]
    public string CEP { get; set; }
    public bool Status { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataModificacao { get; set; }

    public virtual ICollection<Produto> Produtos { get; set; }
}

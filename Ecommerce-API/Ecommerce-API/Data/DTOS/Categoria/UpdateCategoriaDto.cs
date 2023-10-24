using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Data.DTOS;
public class UpdateCategoriaDto
{
    [Required] 
    [StringLength(128, ErrorMessage = "O campo deve conter até 128 caracteres")]
    [RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s]+$", ErrorMessage = "O campo deve conter apenas caracteres do alfabeto.")]
    public string Nome { get; set; }
    public bool Status { get; set; }
    public DateTime DataModificacao { get; set; } = DateTime.Now;
     

}

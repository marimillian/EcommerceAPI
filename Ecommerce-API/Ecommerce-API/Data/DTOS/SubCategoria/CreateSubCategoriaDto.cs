using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Data.DTOS.SubCategoria;

public class CreateSubCategoriaDto
{
   
    [Required(ErrorMessage = "O campo deve ser obrigatório")]
    [StringLength(128, ErrorMessage = "O campo deve conter até 128 caracteres")]
    [RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s]+$", ErrorMessage = "O campo deve conter apenas caracteres do alfabeto.")]
    public string Nome { get; set; }
    public bool Status { get; set; } = true;
    public DateTime DataCriacao { get; set; } = DateTime.Now;

    [Required]
    public int CategoriaId { get; set; }

}

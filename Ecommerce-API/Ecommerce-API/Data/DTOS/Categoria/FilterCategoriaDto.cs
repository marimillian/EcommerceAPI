using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Data.DTOS.Categoria
{
    public class FilterCategoriaDto
    {
        [RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s]+$", ErrorMessage = "O campo deve conter apenas caracteres do alfabeto.")]
        [MinLength(3, ErrorMessage = "O campo não pode ter menos que 3 caracteres")]
        [StringLength(128, ErrorMessage = "O campo deve conter até 128 caracteres")]
        public string? Nome { get; set; }
        public bool? Status { get; set; }
        public bool? Desc { get; set; }
    }
}

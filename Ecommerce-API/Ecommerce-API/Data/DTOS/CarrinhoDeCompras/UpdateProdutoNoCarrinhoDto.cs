using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Data.DTOS.CarrinhoDeCompras
{
    public class UpdateProdutoNoCarrinhoDto
    {
        [Required]
        public int Quantidade { get; set; }
        
    }
}

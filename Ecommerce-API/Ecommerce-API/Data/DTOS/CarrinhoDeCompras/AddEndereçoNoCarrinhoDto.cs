using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Data.DTOS.CarrinhoDeCompras
{
    public class AddEndereçoNoCarrinhoDto
    {
        [Required]
        public string CEP { get; set; }
        [Required]
        public int Numero { get; set; }
        [Required]
        public string Complemento { get; set; }
       
    }
}

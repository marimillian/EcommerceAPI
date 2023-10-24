namespace Ecommerce_API.Models
{
    public class CartaoCreditoModel
    {
        public int CarrinhoId { get; set; }
        public string CPF { get; set; }
        public int NumeroCartao { get; set; }
        public int CVV { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}

namespace Ecommerce_API.Data.DTOS.FinalizacaoCompra
{
    public class ReadCartaoCredito
    {
        public int CarrinhoId { get; set; }
        public string CPF { get; set; }
        public string FormaPagamento { get; set; } = "Cartão de Crédito";
        public string Parcelamento { get; set; }
        public string ValorTotal { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }


    }
}

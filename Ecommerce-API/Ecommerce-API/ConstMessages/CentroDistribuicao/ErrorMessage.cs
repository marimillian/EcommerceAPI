namespace Ecommerce_API.ConstMessages.CentroDistribuicao
{
    public class ErrorMessage
    {
        public const string categoriaExistente = "Já existe uma Categoria com o mesmo nome.";
        public const string subcategoriaExistente = "Já existe uma Subcategoria com o mesmo nome.";
        public const string produtoExistente = "Já existe um Produto com o mesmo nome.";
        public const string centroExistente = "Já existe um Centro de Distribuição com o mesmo nome.";
        public const string enderecoExistente = "Já existe um Centro de Distribuição com o mesmo endereço.";
        public const string produtoAtivo = "Há produtos ativos neste Centro de Distribuição, por este motivo, não é possível inativá-lo.";
        public const string cepHifen = "Por favor, digite o CEP sem o '-' (hífen).";
        public const string cepIncorreto = "O CEP digitado está incorreto.";
    }
}

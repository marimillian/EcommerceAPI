namespace Ecommerce_API.ConstMessages.Subcategoria
{
    public class ErrorMessage
    {
        public const string categoriaExistente = "Já existe uma Categoria com o mesmo nome.";
        public const string subcategoriaExistente = "Já existe uma Subcategoria com o mesmo nome.";
        public const string produtoExistente = "Já existe um Produto com o mesmo nome.";
        public const string centroExistente = "Já existe um Centro de Distribuição com o mesmo nome.";
        public const string produtoAtivo = "Há produtos ativos nesta Subcategoria, por este motivo, não é possível inativá-la.";
        public const string categoriaInativa = "A Categoria está inativa, por este motivo, não é possível editar a Subcategoria.";
    }
}

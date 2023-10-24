using Microsoft.AspNetCore.Identity;


namespace UsuariosApi.Modelo
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        private string _nome;
        public string? Nome { get { return _nome; } set { _nome = value.ToUpper(); } }
        public string? CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? CEP { get; set; }
        public string? Logradouro { get; set; }
        public int Numero { get; set; }
        public string? Bairro { get; set; }
        public string? Complemento { get; set; }
        public string? Localidade { get; set; }
        public string? UF { get; set; }
        public bool Status { get; set; }
        public string? Perfil { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}

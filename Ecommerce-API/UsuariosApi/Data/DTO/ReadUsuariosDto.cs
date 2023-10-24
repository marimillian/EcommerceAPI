namespace UsuariosApi.Data.DTO
{
    public class ReadUsuariosDto
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string DataDeNascimento { get; set; }
        public string Email { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public bool Status { get; set; }
        public string Perfil { get; set; }
        public string DataDeCadastro { get; set; }
        public string DataDeAtualizacao { get; set; }

    }
}

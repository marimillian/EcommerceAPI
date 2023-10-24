namespace Ecommerce_API.Data.DTOS.CentroDistribuicao;

public class FilterCentroDistribuicaoDto
{
    public string? Nome { get; set; }
    public string? Logradouro { get; set; }
    public int? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Localidade { get; set; }
    public string? UF { get; set; }
    public string? CEP { get; set; }
    public bool? Status { get; set; }
    public bool? Desc { get; set; }
    public int? Skip { get; set; } = 0;
    public int? Take { get; set; } = 20;

}

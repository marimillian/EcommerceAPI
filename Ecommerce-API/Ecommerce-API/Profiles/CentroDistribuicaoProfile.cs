using AutoMapper;
using Ecommerce_API.Data.DTOS.CentroDistribuicao;
using Ecommerce_API.Models;

namespace Ecommerce_API.Profiles;

public class CentroDistribuicaoProfile : Profile
{
    public CentroDistribuicaoProfile()
    {
        CreateMap<CreateCentroDistribuicaoDto, CentroDistribuicao>();
        CreateMap<Endereco, CreateCentroDistribuicaoDto>();        
        CreateMap<UpdateCentroDistribuicaoDto, CentroDistribuicao>();
        CreateMap<Endereco, UpdateCentroDistribuicaoDto>();
        CreateMap<CentroDistribuicao, ReadCentroDistribuicaoDto>()
        .ForMember(centroDto => centroDto.DataDeCriacao, opts => opts.MapFrom(centro => centro.DataCriacao.ToString("dd/MM/yyyy HH:mm:ss")))
        .ForMember(centroDto => centroDto.DataDeModificacao, opts => opts.MapFrom(centro => centro.DataModificacao == default
            ? "Não houve alterações."
            : centro.DataModificacao.ToString("dd/MM/yyyy HH:mm:ss")));
    }
}

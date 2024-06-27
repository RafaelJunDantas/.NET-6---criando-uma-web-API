using AutoMapper;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class SessaoProfile : Profile
{
    public SessaoProfile()
    {
        CreateMap<CreateSessaoDTO, Sessao>();
        CreateMap<Sessao, ReadSessaoDTO>()
            .ForMember(sessaoDTO => sessaoDTO.filme,
                opt => opt.MapFrom(sessao => sessao.filme));
    }
}

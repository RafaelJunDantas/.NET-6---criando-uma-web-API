﻿using AutoMapper;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<CreateFilmeDTO, Filme>();
        CreateMap<UpdateFilmeDTO, Filme>();
        CreateMap<Filme, UpdateFilmeDTO>();
        CreateMap<Filme, ReadFilmeDTO>()
            .ForMember(filmeDTO => filmeDTO.sessoes,
                opt => opt.MapFrom(filme => filme.sessoes)); ;
    }
}

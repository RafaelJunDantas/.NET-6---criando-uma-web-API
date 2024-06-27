﻿using AutoMapper;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        CreateMap<CreateCinemaDTO, Cinema>();
        CreateMap<UpdateCinemaDTO, Cinema>();
        CreateMap<Cinema, UpdateCinemaDTO>();
        CreateMap<Cinema, ReadCinemaDTO>()
            .ForMember(cinemaDTO => cinemaDTO.endereco, 
                opt => opt.MapFrom(cinema => cinema.endereco));
    }
}

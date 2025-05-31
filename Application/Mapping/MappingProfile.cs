using Application.DTOs;
using AutoMapper;
using Domain.Entity;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Pessoa, PessoaDTO>();
            CreateMap<PessoaDTO, Pessoa>();
            CreateMap<Jogo, JogoDTO>();
            CreateMap<JogoDTO, Jogo>();
        }
    }
}

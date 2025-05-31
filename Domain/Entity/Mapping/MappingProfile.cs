using AutoMapper;
using Domain.Entity.DTOs;

namespace Domain.Entity.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Pessoa, PessoaDTO>();
            CreateMap<PessoaDTO, Pessoa>();
        }
    }
}

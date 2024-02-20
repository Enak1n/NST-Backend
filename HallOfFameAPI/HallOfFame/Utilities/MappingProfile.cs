using AutoMapper;
using HallOfFame.Domain.Entities;
using HallOfFame.Infrastructure.DTO;

namespace HallOfFame.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonResponse>();
            CreateMap<Skill, SkillResponse>();

            CreateMap<PersonRequest, Person>()
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DispayName))
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills));

            CreateMap<SkillRequest, Skill>()
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
                .ForMember(dest => dest.Persons, opt => opt.Ignore());
        }
    }
}

using AutoMapper;
using WorldAPI.DTO.Country;
using WorldAPI.DTO.States;
using WorldAPI.Models;

namespace WorldAPI.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            //Source,Destination
            CreateMap<CreateCountryDto,Country>();
            CreateMap<Country,CountryDto>();
            CreateMap<UpdateCountryDto,Country>();

            CreateMap<CreateStatesDto, States>();
            CreateMap<States, StatesDto>();
            CreateMap<UpdateStatesDto, States>();


        }
    }
}

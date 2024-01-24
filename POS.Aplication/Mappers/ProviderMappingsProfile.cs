using AutoMapper;
using POS.Aplication.Dtos.Provider.Request;
using POS.Aplication.Dtos.Provider.Response;
using POS.Domain.Entities;
using POS.Utilites.Static;

namespace POS.Aplication.Mappers
{
    public class ProviderMappingsProfile : Profile
    {
        public ProviderMappingsProfile()
        {
            CreateMap<Provider, ProviderResponseDto>()
                .ForMember(x => x.ProviderId, x => x.MapFrom(y => y.Id))
                .ForMember(x=>x.DocumentType,x=>x.MapFrom(y=>y.DocumentType.Abbreviation))
                .ForMember(x => x.StateProvider, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();
            
            CreateMap<Provider, ProviderByIdResponseDto>()
                .ForMember(x => x.ProviderId, x => x.MapFrom(y => y.Id))
                .ReverseMap();
            CreateMap<ProviderRequestDto, Provider>();
        }
    }
}

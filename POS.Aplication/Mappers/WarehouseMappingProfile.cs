using AutoMapper;
using POS.Aplication.Comnons.Bases.Select.Response;
using POS.Aplication.Dtos.Warehouse.Request;
using POS.Aplication.Dtos.Warehouse.Response;
using POS.Domain.Entities;
using POS.Utilites.Static;

namespace POS.Aplication.Mappers
{
    public class WarehouseMappingProfile : Profile
    {
        public WarehouseMappingProfile()
        {
            CreateMap<Warehouse, WarehouseResponseDto>()
                .ForMember(x => x.WarehouseId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.StateWarehouse, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<Warehouse, SelectResponse>()
                .ForMember(x => x.Description, x => x.MapFrom(y => y.Name))
                .ReverseMap();

            CreateMap<Warehouse, WarehouseByIdResponseDto>().
                ForMember(x => x.WarehouseId, x => x.MapFrom(y => y.Id))
                .ReverseMap();

            CreateMap<WarehouseRequestDto, Warehouse>();
        }
    }
}

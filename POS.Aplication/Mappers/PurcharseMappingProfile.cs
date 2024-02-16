using AutoMapper;
using POS.Aplication.Dtos.Purcharse.Response;
using POS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Aplication.Mappers
{
    public class PurcharseMappingProfile : Profile
    {
        public PurcharseMappingProfile()
        {
            CreateMap<Purcharse, PurcharseResponseDto>()
                .ForMember(x => x.PurcharseId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.Provider, x => x.MapFrom(y => y.Provider!.Name))
                .ForMember(x => x.Warehouse, x => x.MapFrom(y => y.Warehouse.Name))
                .ForMember(x => x.DateOfPurcharse, x => x.MapFrom(y => y.AuditCreateDate))
                .ReverseMap();

        }
    }
}

using AutoMapper;
using POS.Aplication.Dtos.Product.Request;
using POS.Aplication.Dtos.Product.Response;
using POS.Domain.Entities;
using POS.Utilites.Static;

namespace POS.Aplication.Mappers
{
    public class ProductMappingsProfile:Profile
    {
        public ProductMappingsProfile()
        {
            CreateMap<Product, ProductResponseDto>()
                .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.Category, x => x.MapFrom(y => y.Category.Name))
                .ForMember(x => x.StateProduct, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<Product, ProductByIdResponseDto>()
                .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))
                .ReverseMap();

            CreateMap<ProductRequestDto, Product>();
        }
    }
}

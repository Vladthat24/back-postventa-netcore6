﻿using AutoMapper;
using POS.Aplication.Comnons.Bases.Select.Response;
using POS.Aplication.Dtos.Category.Request;
using POS.Aplication.Dtos.Category.Response;
using POS.Domain.Entities;
using POS.Utilites.Static;

namespace POS.Aplication.Mappers
{
    public class CategoryMappingsProfile: Profile
    {
        public CategoryMappingsProfile()
        {

            CreateMap<Category, CategoryResponseDto>()
                .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.StateCategory, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<CategoryRequestDto, Category>();

            CreateMap<Category, SelectResponse>()
                .ForMember(x => x.Description, x => x.MapFrom(y => y.Name))
                .ReverseMap();
        }
    }
}

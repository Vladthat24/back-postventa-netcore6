using AutoMapper;
using POS.Aplication.Dtos.User.Request;
using POS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Aplication.Mappers
{
    public class UserMappingsProfile: Profile
    {
        public UserMappingsProfile()
        {
            CreateMap<UserRequestDto, User>();
            CreateMap<TokenRequestDto, User>();
        }

    }
}

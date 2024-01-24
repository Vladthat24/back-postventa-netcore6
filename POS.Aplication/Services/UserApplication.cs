using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using POS.Aplication.Comnons.Bases.Response;
using POS.Aplication.Dtos.User.Request;
using POS.Aplication.Interfaces;
using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilites.Static;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace POS.Aplication.Services
{
    public class UserApplication : IUserApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public UserApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> RegisterUser(UserRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var account = _mapper.Map<User>(requestDto);
            account.Password = BC.HashPassword(account.Password);

            response.Data = await _unitOfWork.User.RegisterAsync(account);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FALIED;
            }

            return response;
            
        }


    }
}

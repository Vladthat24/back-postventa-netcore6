using POS.Aplication.Comnons.Bases.Response;
using POS.Aplication.Dtos.User.Request;

namespace POS.Aplication.Interfaces
{
    public interface IAuthApplication
    {
        Task<BaseResponse<string>> Login(TokenRequestDto requestDto,string authType);
        Task<BaseResponse<string>> LoginWithGoogle(string credentials, string authType);
    }
}

using POS.Aplication.Commons.Bases.Request;
using POS.Aplication.Comnons.Bases.Response;
using POS.Aplication.Comnons.Bases.Select.Response;
using POS.Aplication.Dtos.Provider.Request;
using POS.Aplication.Dtos.Provider.Response;

namespace POS.Aplication.Interfaces
{
    public interface IProviderApplication
    {
        Task<BaseResponse<IEnumerable<ProviderResponseDto>>> ListProviders(BaseFilterRequest filters);
        Task<BaseResponse<IEnumerable<SelectResponse>>> ListSelectProviders();
        Task<BaseResponse<ProviderByIdResponseDto>> ProviderById(int providerId);

        Task<BaseResponse<bool>> RegisterProvider(ProviderRequestDto requestDto);
        Task<BaseResponse<bool>> EditProvider(int providerId, ProviderRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveProvider(int providerId);
    }
}

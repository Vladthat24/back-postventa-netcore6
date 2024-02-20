using POS.Aplication.Commons.Bases.Request;
using POS.Aplication.Comnons.Bases.Response;
using POS.Aplication.Dtos.Purcharse.Request;
using POS.Aplication.Dtos.Purcharse.Response;

namespace POS.Aplication.Interfaces
{
    public interface IPurcharseApplication
    {
        Task<BaseResponse<IEnumerable<PurcharseResponseDto>>> ListPurcharse(BaseFilterRequest filters);
        Task<BaseResponse<PurcharseByIdResponseDto>> PurcharseById(int purcharseId);
        Task<BaseResponse<bool>> RegisterPurcharse(PurcharseRequestDto requestDto);
    }
}

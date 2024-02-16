using POS.Aplication.Commons.Bases.Request;
using POS.Aplication.Comnons.Bases.Response;
using POS.Aplication.Dtos.Purcharse.Response;

namespace POS.Aplication.Interfaces
{
    public interface IPurcharseApplication
    {
        Task<BaseResponse<IEnumerable<PurcharseResponseDto>>> ListPurcharse(BaseFilterRequest filters);
    }
}

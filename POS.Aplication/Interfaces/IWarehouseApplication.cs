using POS.Aplication.Commons.Bases.Request;
using POS.Aplication.Comnons.Bases.Response;
using POS.Aplication.Dtos.Warehouse.Request;
using POS.Aplication.Dtos.Warehouse.Response;

namespace POS.Aplication.Interfaces
{
    public interface IWarehouseApplication
    {
        Task<BaseResponse<IEnumerable<WarehouseResponseDto>>> ListWarehouses(BaseFilterRequest filters);
        Task<BaseResponse<WarehouseByIdResponseDto>> WarehouseById(int warehouseId);
        Task<BaseResponse<bool>> RegisterWareHouse(WarehouseRequestDto requestDto);
        Task<BaseResponse<bool>> EditWarehouse(int warehouseId, WarehouseRequestDto requestDto);

        Task<BaseResponse<bool>> RemoveWarehouse(int warehouseId);
    }
}

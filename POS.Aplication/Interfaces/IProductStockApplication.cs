using POS.Aplication.Comnons.Bases.Response;
using POS.Aplication.Dtos.ProductStock;

namespace POS.Aplication.Interfaces
{
    public interface IProductStockApplication
    {
        Task<BaseResponse<IEnumerable<ProductStockByWarehouseResponseDto>>> GetProductStockByWarehouseAsync(int productId);
    }
}

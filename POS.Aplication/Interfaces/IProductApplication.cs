using POS.Aplication.Commons.Bases.Request;
using POS.Aplication.Comnons.Bases.Response;
using POS.Aplication.Dtos.Product.Request;
using POS.Aplication.Dtos.Product.Response;
using POS.Domain.Entities;

namespace POS.Aplication.Interfaces
{
    public interface IProductApplication
    {
        Task<BaseResponse<IEnumerable<ProductResponseDto>>> ListProducts(BaseFilterRequest filters);
        Task<BaseResponse<ProductByIdResponseDto>> ProductById(int productId);
        Task<BaseResponse<bool>> RegisterProduct(ProductRequestDto requestDto);
        Task<BaseResponse<bool>> EditProduct(int ProductId, ProductRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveProduct(int productId);
    }
}

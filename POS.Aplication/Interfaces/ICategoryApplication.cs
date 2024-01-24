using POS.Aplication.Commons.Bases.Request;
using POS.Aplication.Comnons.Bases.Response;
using POS.Aplication.Dtos.Category.Request;
using POS.Aplication.Dtos.Category.Response;


namespace POS.Aplication.Interfaces
{
    public interface ICategoryApplication
    {
        Task<BaseResponse<IEnumerable<CategoryResponseDto>>> ListCategories(BaseFilterRequest filters);
        Task<BaseResponse<IEnumerable<CategorySelectResponseDto>>> ListSelectCategories();
        Task<BaseResponse<CategoryResponseDto>> CategoryById(int categoryId);
        Task<BaseResponse<bool>> RegisterCategory(CategoryRequestDto requestDto);
        Task<BaseResponse<bool>> EditCategory(int categoryId, CategoryRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveCategory(int categoryId);

    }
}

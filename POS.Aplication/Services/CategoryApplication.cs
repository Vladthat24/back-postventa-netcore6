using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POS.Aplication.Commons.Bases.Request;
using POS.Aplication.Comnons.Bases.Response;
using POS.Aplication.Comnons.Orderning;
using POS.Aplication.Dtos.Category.Request;
using POS.Aplication.Dtos.Category.Response;
using POS.Aplication.Interfaces;
using POS.Aplication.Validations.Category;
using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilites.Static;
using WatchDog;

namespace POS.Aplication.Services
{
    public class CategoryApplication : ICategoryApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CategoryValidator _validationRules;
        private readonly IOrderingQuery _orderingQuery;

        public CategoryApplication(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            CategoryValidator validationRules,
            IOrderingQuery orderingQuery
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
            _orderingQuery = orderingQuery;
        }


        public async Task<BaseResponse<IEnumerable<CategoryResponseDto>>> ListCategories(BaseFilterRequest filters)
        {
            var response=new BaseResponse<IEnumerable<CategoryResponseDto>>();
            try
            {
                var categories = _unitOfWork.Category.GetAllQueryable();

                if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.Textfilter))
                {
                    switch (filters.NumFilter)
                    {
                        case 1:
                            categories = categories.Where(x => x.Name!.Contains(filters.Textfilter));
                            break;
                        case 2:
                            categories = categories.Where(x => x.Description!.Contains(filters.Textfilter));
                            break;
                    }
                }

                if (filters.stateFilter is not null)
                {
                    categories = categories.Where(x => x.State.Equals(filters.stateFilter));
                }
                if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
                {
                    categories = categories.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
                }

                if (filters.Sort is null)
                {
                    filters.Sort = "Id";
                }
                var items= await _orderingQuery.Orderning(filters, categories, !(bool)filters.Download!).ToListAsync();

                response.IsSuccess = true;
                response.TotalRecords= await categories.CountAsync();
                response.Data = _mapper.Map<IEnumerable<CategoryResponseDto>>(items);
                response.Message = ReplyMessage.MESSAGE_QUERY;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }          
            return response;
        }

        public async Task<BaseResponse<IEnumerable<CategorySelectResponseDto>>> ListSelectCategories()
        {
            var response=new BaseResponse<IEnumerable<CategorySelectResponseDto>>();
            var categories = await _unitOfWork.Category.GetAllAsync();

            if(categories is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<CategorySelectResponseDto>>(categories);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }

        public async Task<BaseResponse<CategoryResponseDto>> CategoryById(int categoryId)
        {
            var response = new BaseResponse<CategoryResponseDto>();
            var category = await _unitOfWork.Category.GetByIdAsync(categoryId);

            if(category is not null)
            {
                response.IsSuccess = true;
                response.Data= _mapper.Map<CategoryResponseDto>(category);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
        public async Task<BaseResponse<bool>> RegisterCategory(CategoryRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var validationResult = await _validationRules.ValidateAsync(requestDto);

            if(!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errores = validationResult.Errors;
                return response;
            }

            var category = _mapper.Map<Category>(requestDto);
            response.Data = await _unitOfWork.Category.RegisterAsync(category);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;

            }else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FALIED;

            }

            return response;
        }

        public async Task<BaseResponse<bool>> EditCategory(int categoryId, CategoryRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var categoryEdit = await CategoryById(categoryId);

            if(categoryEdit.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            var category = _mapper.Map<Category>(requestDto);
            category.Id = categoryId;
            response.Data = await _unitOfWork.Category.EditAsync(category);
            if(response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FALIED;
            }

            return response;
        }
        public async Task<BaseResponse<bool>> RemoveCategory(int categoryId)
        {
            var response=new BaseResponse<bool>();
            var category= await CategoryById(categoryId);

            if(category.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            response.Data = await _unitOfWork.Category.RemoveAsync(categoryId);

            if(response.Data )
            {
                response.IsSuccess= true;
                response.Message = ReplyMessage.MESSAGE_DELETE;

            }
            else
            {
                response.IsSuccess = false;
                response.Message=ReplyMessage.MESSAGE_FALIED;
            }

            return response;
        }
    }
}

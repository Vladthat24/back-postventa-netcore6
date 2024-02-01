using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POS.Aplication.Commons.Bases.Request;
using POS.Aplication.Comnons.Bases.Response;
using POS.Aplication.Comnons.Orderning;
using POS.Aplication.Dtos.Provider.Request;
using POS.Aplication.Dtos.Provider.Response;
using POS.Aplication.Interfaces;
using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilites.Static;

namespace POS.Aplication.Services
{
    public class ProviderApplication : IProviderApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderingQuery _orderingQuery;



        public ProviderApplication(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery orderingQuery)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderingQuery = orderingQuery;
        }


        public async Task<BaseResponse<IEnumerable<ProviderResponseDto>>> ListProviders(BaseFilterRequest filters)
        {
            var response =  new BaseResponse<IEnumerable<ProviderResponseDto>>();

            try
            {
                var providers = _unitOfWork.Provider.GetAllQueryable().Include(x=>x.DocumentType).AsQueryable();

                if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.Textfilter))
                {
                    switch (filters.NumFilter)
                    {
                        case 1:
                            providers = providers.Where(x => x.Name!.Contains(filters.Textfilter));
                            break;
                        case 2:
                            providers = providers.Where(x => x.Email!.Contains(filters.Textfilter));
                            break;
                        case 3:
                            providers = providers.Where(x => x.DocumentNumber!.Contains(filters.Textfilter));
                            break;
                    }
                }

                if (filters.StateFilter is not null)
                {
                    providers = providers.Where(x => x.State.Equals(filters.StateFilter));
                }
                if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
                {
                    providers = providers.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) &&
                                                     x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate)
                                                     .AddDays(1));
                }

                if (filters.Sort is null)
                {
                    filters.Sort = "Id";
                }

                var items = await _orderingQuery.Orderning(filters, providers, !(bool)filters.Download!).ToListAsync();

                response.IsSuccess = true;
                response.TotalRecords = await providers.CountAsync();
                response.Data = _mapper.Map<IEnumerable<ProviderResponseDto>>(items);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            catch (Exception ex)
            {
                response.IsSuccess=false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }

            return response;

        }

        public async Task<BaseResponse<ProviderByIdResponseDto>> ProviderById(int providerId)
        {
            var response= new BaseResponse<ProviderByIdResponseDto>();

            try
            {
                var provider = await _unitOfWork.Provider.GetByIdAsync(providerId);

                if (provider is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<ProviderByIdResponseDto>(provider);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterProvider(ProviderRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var provider = _mapper.Map<Provider>(requestDto);

            try
            {
                response.Data = await _unitOfWork.Provider.RegisterAsync(provider);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_SAVE;
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_FALIED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<bool>> EditProvider(int providerId, ProviderRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var providerByid = await ProviderById(providerId);

                if (providerByid.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }
                var provider = _mapper.Map<Provider>(requestDto);
                provider.Id = providerId;
                response.Data = await _unitOfWork.Provider.EditAsync(provider);

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
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RemoveProvider(int providerId)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var providerById = await ProviderById(providerId);

                if (providerById.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.Data = await _unitOfWork.Provider.RemoveAsync(providerId);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_DELETE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FALIED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }

            return response;
        }
    }
}

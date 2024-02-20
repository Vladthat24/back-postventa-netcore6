using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POS.Aplication.Commons.Bases.Request;
using POS.Aplication.Comnons.Bases.Response;
using POS.Aplication.Comnons.Orderning;
using POS.Aplication.Dtos.Purcharse.Request;
using POS.Aplication.Dtos.Purcharse.Response;
using POS.Aplication.Interfaces;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilites.Static;

namespace POS.Aplication.Services
{
    internal class PurcharseApplication : IPurcharseApplication
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
        private readonly IOrderingQuery _orderingQuery;

        public PurcharseApplication(IUnitOfWork unitOfwork, IMapper mapper, IOrderingQuery orderingQuery)
        {
            _unitOfwork = unitOfwork;
            _mapper = mapper;
            _orderingQuery = orderingQuery;
        }


        public async Task<BaseResponse<IEnumerable<PurcharseResponseDto>>> ListPurcharse(BaseFilterRequest filters)
        {
            var response = new BaseResponse<IEnumerable<PurcharseResponseDto>>();

            try
            {
                var purcharses = _unitOfwork.Purcharse.GetAllQueryable().Include(x => x.Provider).Include(x=>x.Warehouse).AsQueryable();

                if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.Textfilter))
                {
                    switch (filters.NumFilter)
                    {
                        case 1:
                            purcharses = purcharses.Where(x => x.Provider!.Name!.Contains(filters.Textfilter));
                            break;
                    }
                }

                if (filters.StateFilter is not null)
                {
                    purcharses = purcharses.Where(x => x.State.Equals(filters.StateFilter));
                }
                if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
                {
                    purcharses = purcharses.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate)
                    && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate));
                }

                filters.Sort ??= "Id";

                var items = await _orderingQuery.Orderning(filters, purcharses, !(bool)filters.Download!).ToListAsync();

                response.IsSuccess = true;
                response.TotalRecords = await purcharses.CountAsync();
                response.Data = _mapper.Map<IEnumerable<PurcharseResponseDto>>(items);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<PurcharseByIdResponseDto>> PurcharseById(int purcharseId)
        {
            var response = new BaseResponse<PurcharseByIdResponseDto>();
            try
            {
                var purcharse= await _unitOfwork.Purcharse.GetByIdAsync(purcharseId);
                if(purcharse is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var purcharseDetails = await _unitOfwork.PurcharseDetail.GetPurcharseDetailByPurcharseId(purcharse.Id);

                purcharse.PurcharseDetails = purcharseDetails.ToList();

                response.IsSuccess = true;
                response.Data = _mapper.Map<PurcharseByIdResponseDto>(purcharse);
                response.Message = ReplyMessage.MESSAGE_QUERY;

                return response;

            }catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<bool>> RegisterPurcharse(PurcharseRequestDto requestDto)
        {
            var response= new BaseResponse<bool>();

            try
            {

            }catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }
    }
}

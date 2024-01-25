using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POS.Aplication.Commons.Bases.Request;
using POS.Aplication.Comnons.Bases.Response;
using POS.Aplication.Comnons.Orderning;
using POS.Aplication.Dtos.Warehouse.Response;
using POS.Aplication.Interfaces;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilites.Static;
using WatchDog;

namespace POS.Aplication.Services
{
    public class WarehouseApplication : IWarehouseApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderingQuery _orderingQuery;

        public WarehouseApplication(IOrderingQuery orderingQuery, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _orderingQuery = orderingQuery;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<WarehouseResponseDto>>> ListWarehouses(BaseFilterRequest filters)
        {
            var response= new  BaseResponse<IEnumerable<WarehouseResponseDto>>();
            try
            {
                var warehouses = _unitOfWork.Warehouse.GetAllQueryable();
                if(filters.NumFilter is not null && !string.IsNullOrEmpty(filters.Textfilter))
                {
                    switch (filters.NumFilter)
                    {
                        case 1:
                            warehouses = warehouses.Where(x => x.Name!.Contains(filters.Textfilter));
                            break;
                    }
                }
                if (filters.stateFilter is not null)
                {
                    warehouses = warehouses.Where(x => x.State.Equals(filters.stateFilter));
                }
                if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
                {
                    warehouses = warehouses.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
                }

                if (filters.Sort is null)
                {
                    filters.Sort = "Id";
                }
                var items = await _orderingQuery.Orderning(filters, warehouses, !(bool)filters.Download!).ToListAsync();

                response.IsSuccess = true;
                response.TotalRecords = await warehouses.CountAsync();
                response.Data = _mapper.Map<IEnumerable<WarehouseResponseDto>>(items);
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
    }
}

using POS.Aplication.Commons.Bases.Request;
using System.Linq.Dynamic.Core;

namespace POS.Aplication.Comnons.Orderning
{
    public class OrderingQuery : IOrderingQuery
    {
        public IQueryable<TDTO> Orderning<TDTO>(BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination = false) where TDTO : class
        {
            IQueryable<TDTO> queryDto = request.Order == "desc" ? queryable.OrderBy($"{request.Sort} descending") : queryable.OrderBy($"{request.Sort} ascending");

            if (pagination) queryDto = queryDto.Paginate(request);

            return queryDto;
        }
    }
}

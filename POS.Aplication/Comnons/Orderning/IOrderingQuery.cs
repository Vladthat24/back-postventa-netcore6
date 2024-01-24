using POS.Aplication.Commons.Bases.Request;

namespace POS.Aplication.Comnons.Orderning
{
    public interface IOrderingQuery
    {
        IQueryable<TDTO> Orderning<TDTO>(BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination = false) where TDTO : class;
    }
}

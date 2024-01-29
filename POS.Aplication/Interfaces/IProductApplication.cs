using POS.Aplication.Comnons.Bases.Response;
using POS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Aplication.Interfaces
{
    public interface IProductApplication
    {
        Task<BaseResponse<IEnumerable<Product>>>
    }
}

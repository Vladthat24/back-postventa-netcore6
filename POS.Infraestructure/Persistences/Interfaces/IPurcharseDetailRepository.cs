using POS.Domain.Entities;

namespace POS.Infraestructure.Persistences.Interfaces
{
    public interface IPurcharseDetailRepository
    {
        Task<IEnumerable<PurcharseDetail>> GetPurcharseDetailByPurcharseId(int purcharseId);
    }
}

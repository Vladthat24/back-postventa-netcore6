using POS.Domain.Entities;
using System.Data;

namespace POS.Infraestructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //Declaracion o matricula de nuestra interfaces a nivel de repository
        public IGenericRepository<Category> Category { get; }
        public IGenericRepository<Provider> Provider { get; }
        public IGenericRepository<DocumentType> DocumentType { get; }

        public IUserRepository User { get; }
        public IWarehouseRepository Warehouse { get; }
        public IGenericRepository<Product> Product { get; }

        public IProductStockRepository ProductStock { get; }
        public void SaveChanges();

        public Task SaveChangesAsync();

        //Transaction para validar el "OK" de todas las tareas o hacer rollback
        IDbTransaction BeginTransaction();

    }
}

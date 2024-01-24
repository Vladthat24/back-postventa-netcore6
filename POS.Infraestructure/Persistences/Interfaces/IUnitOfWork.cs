using POS.Domain.Entities;

namespace POS.Infraestructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //Declaracion o matricula de nuestra interfaces a nivel de repository
        public IGenericRepository<Category> Category { get; }
        public IGenericRepository<Provider> Provider { get; }
        public IGenericRepository<DocumentType> DocumentType { get; }

        public IUserRepository User { get; }

        public void SaveChanges();

        public Task SaveChangesAsync();

    }
}

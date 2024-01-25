﻿using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly POSContext _context;



        public IUserRepository _user = null!;

        public IGenericRepository<Category> _category = null!;

        public IGenericRepository<Provider> _provider = null!;

        public IGenericRepository<DocumentType> _documentType = null!;
        public IWarehouseRepository _warehouse = null!;

        public UnitOfWork(POSContext context)
        {
            _context = context;
        }

        public IGenericRepository<Category> Category => _category ?? new GenericRepository<Category>(_context);

        public IGenericRepository<Provider> Provider => _provider ?? new GenericRepository<Provider>(_context);

        public IGenericRepository<DocumentType> DocumentType => _documentType ?? new GenericRepository<DocumentType>(_context);

        public IUserRepository User => _user ?? new UserRepository(_context);

        public IWarehouseRepository Warehouse => _warehouse?? new WarehouseRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            //GC.SuppressFinalize(this);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly POSContext _context;

        public UserRepository(POSContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<User> UserByEmail(string email)
        {
            var account = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x=>x.Email!.Equals(email));
            return account!;
            
        }
    }
}

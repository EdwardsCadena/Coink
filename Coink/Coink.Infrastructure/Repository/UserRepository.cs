using Coink.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coink.Infrastructure.Repository
{
    public class UserRepository : Core.Interfaces.IUserRepository
    {
        private readonly CoinkContext _context;

        public UserRepository(CoinkContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.FromSqlRaw("EXEC GetAllUsers").ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FromSqlRaw("EXEC GetUserById {0}", id).FirstOrDefaultAsync();
        }

        public async Task InsertUser(User user)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC InsertUser {0}, {1}, {2}, {3}",
                user.Name, user.PhoneNumber, user.Address, user.MunicipalityId);
        }

        public async Task UpdateUser(User user)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC UpdateUser {0}, {1}, {2}, {3}, {4}",
                user.Id, user.Name, user.PhoneNumber, user.Address, user.MunicipalityId);
        }

        public async Task DeleteUser(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteUser {0}", id);
        }
    }
}
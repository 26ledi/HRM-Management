using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskDbContext _context;

        public UserRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User task)
        {
            _context.Users.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.Include(x => x.Tasks)
                                           .ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.AsNoTracking().Include(x => x.Tasks)
                                           .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.Include(x => x.Tasks)
                                           .FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}

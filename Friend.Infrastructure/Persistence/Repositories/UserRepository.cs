using Friend.Application.Contracts.Persistence;
using Friend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Infrastructure.Persistence.Repositories
{
    public class UserRepository(AppDbContext _context) : IUserRepository
    {
        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task AddAsync(AppUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<AppUser?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }


        public async Task<List<AppUser>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser?> GetUserByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }


    }
}

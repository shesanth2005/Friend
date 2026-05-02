using Friend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Application.Contracts.Persistence
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string email);
        Task AddAsync(AppUser user);
        Task<AppUser?> GetByEmailAsync(string email);
        Task<List<AppUser>> GetAllUsersAsync();
        Task<AppUser?> GetUserByIdAsync(string id);
        
    }
}

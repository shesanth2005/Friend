using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ICustomerRepository Customers { get; }

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<int> SaveChangesAsync();
    }
}

using Friend.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Friend.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;

        public IUserRepository Users { get; }
        public ICustomerRepository Customers { get; }

        //Constructor
        public UnitOfWork(AppDbContext context, IUserRepository users, ICustomerRepository customers)
        {
            _context = context;
            Users = users;
            Customers = customers;
        }

        // Start transaction
        public async Task BeginTransactionAsync() =>
            _transaction = await _context.Database.BeginTransactionAsync();

        // Commit transaction
        public async Task CommitTransactionAsync()
        {
            await _context.SaveChangesAsync();
            if (_transaction is not null)
                await _transaction.CommitAsync();
        }

        // Rollback transaction
        public async Task RollbackTransactionAsync()
        {
            if (_transaction is not null)
                await _transaction.RollbackAsync();
        }

        // Save changes
        public async Task<int> SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}

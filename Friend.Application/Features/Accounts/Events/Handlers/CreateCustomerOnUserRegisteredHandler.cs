using System.Threading;
using System.Threading.Tasks;
using Friend.Application.Contracts.Persistence;
using Friend.Application.Features.Accounts.Events.Notification;
using Friend.Domain.Entities;
using MediatR;

namespace Friend.Application.Features.Accounts.Events.Handlers
{
 
    // Handles the UserRegisteredEvent.
    // This runs after a user is registered and adds a corresponding Customer record.
    
    public class CreateCustomerOnUserRegisteredHandler(IUnitOfWork _unitOfWork) : INotificationHandler<UserRegisteredEvent>
    {
        public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                UserId = notification.User.UserId,
                CustomerName = notification.User.DisplayName,
                ContactEmail = notification.User.Email
            };

            await _unitOfWork.Customers.AddAsync(customer);
        }

    }
}

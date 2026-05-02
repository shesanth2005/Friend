using Friend.Application.Features.Accounts.Dtos;
using MediatR;

namespace Friend.Application.Features.Accounts.Events.Notification
{
    // Event triggered after a user is successfully registered
    // Encapsulates all user info needed for subsequent operations (e.g., creating a customer)
    public record UserRegisteredEvent(UserRegisteredDto User) : INotification;
}


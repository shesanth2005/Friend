using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Application.Features.Accounts.Dtos
{
    public class UserRegisteredDto
    {
        public string UserId { get; init; }
        public string DisplayName { get; init; } = default!;
        public string Email { get; init; } = default!;
    }
}

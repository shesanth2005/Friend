using Friend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Application.Contracts.Security
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}

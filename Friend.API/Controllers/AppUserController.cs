using Friend.Application.Features.Accounts.Queries.GetAllUsers;
using Friend.Application.Features.Accounts.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Friend.API.Controllers
{
    public class AppUserController(IMediator _mediator) : BaseAPIController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()                           //http://localhost:5157/api/AppUser
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)                 //http://localhost:5157/api/AppUser/eeb3f2da-9eb3-49d2-8c6b-5611fe8a8127
        {
            var user = await _mediator.Send(new GetUserByIdQuery { Id = id });
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}

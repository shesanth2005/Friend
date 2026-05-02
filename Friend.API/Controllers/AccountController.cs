using AutoMapper;
using Friend.API.Extensions;
using Friend.API.Models.Requests;
using Friend.Application.Features.Accounts.Commands.LoginUser;
using Friend.Application.Features.Accounts.Commands.RegisterUser;
using Friend.Application.Features.Accounts.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Friend.API.Controllers
{
    public class AccountController(IMediator _mediator, IMapper _mapper) : BaseAPIController
    {


        //[HttpPost("register")]
        //public async Task<ActionResult<UserDto>> Register([FromBody] RegisterRequest request) //http://localhost:5157/api/account/register
        //{
        //    // Automatically map RegisterRequest → RegisterDto
        //    //var command = new RegisterUserCommand
        //    //{
        //    //    RegisterDto = _mapper.Map<RegisterDto>(request)
        //    //};
        //    //var result = await _mediator.Send(command);

        //    var command = _mapper.Map<RegisterUserCommand>(request);
        //    var result = await _mediator.Send(command);

        //    return Ok(result);
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterUserCommand>(request);
            var userDto = await _mediator.Send(command);

            // Map UserDto to ApiResponse<UserDto> using extension
            return this.ToApiResponse(userDto, "User registered successfully");
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginRequest request)  //http://localhost:5157/api/account/login
        {
            var command = _mapper.Map<LoginUserCommand>(request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        //{
        //    "displayName": "John Doe",
        //    "email": "johndoe6@example.com",
        //    "password": "StrongPassword123!"
        //}
}
}

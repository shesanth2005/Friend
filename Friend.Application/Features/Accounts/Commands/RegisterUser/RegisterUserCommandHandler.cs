using AutoMapper;
using Friend.Application.Contracts.Persistence;
using Friend.Application.Contracts.Security;
using Friend.Application.Features.Accounts.Dtos;

using Friend.Application.Features.Accounts.Events.Notification;
using Friend.Domain.Entities;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace Friend.Application.Features.Accounts.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(IMapper _mapper, ITokenService _tokenService, IUnitOfWork _unitOfWork, IMediator _mediator) : IRequestHandler<RegisterUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // 1️ Check if email already exists
            if (await _unitOfWork.Users.UserExistsAsync(request.RegisterDto.Email))
                throw new Exception("Email already registered");

            // 2️ Begin transaction
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // 3️ Create password hash
                using var hmac = new HMACSHA256();
                var user = new AppUser
                {
                    DisplayName = request.RegisterDto.DisplayName,
                    Email = request.RegisterDto.Email,
                    PasswordSalt = hmac.Key,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.RegisterDto.Password))
                };

                // 4️ Add user
                await _unitOfWork.Users.AddAsync(user);


                // 5️ Add corresponding customer

                var userEventDto = _mapper.Map<UserRegisteredDto>(user);
                await _mediator.Publish(new UserRegisteredEvent(userEventDto), cancellationToken);
                

           
              

                // 6️ Commit transaction
                await _unitOfWork.CommitTransactionAsync();

                // 7️ Map to DTO and create token
                var userDto = _mapper.Map<UserDto>(user);
                //userDto.Token = _tokenService.CreateToken(user);

                return userDto;
            }
            catch
            {
                // 8️ Rollback on error
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}

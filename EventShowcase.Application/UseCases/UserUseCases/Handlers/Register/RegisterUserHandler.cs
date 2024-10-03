using EventShowcase.API.Contracts.Users.Requests;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Core.Models;
using EventShowcase.Core.Validators.Create;
using EventShowcase.Infrastructure;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.UseCases.UserUseCases.Handlers.Register
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly UserCreateValidator _validator;


        public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, UserCreateValidator validator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _validator = validator;
        }

        public async Task<Unit> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasher.Generate(request.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.UserName,
                Email = request.Email,
                PasswordHash = hashedPassword
            };


            var validate = _validator.Validate(user);
            if (validate.IsValid)
            {
                await _userRepository.AddUserAsync(user);
                return Unit.Value;
            }
            else
            {
                throw new ValidationException(validate.Errors);
            }
        }
    }
}

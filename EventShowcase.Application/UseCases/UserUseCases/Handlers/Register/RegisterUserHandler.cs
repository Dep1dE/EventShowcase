using AutoMapper;
using EventShowcase.API.Contracts.Users.Requests;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Core.Models;
using EventShowcase.Core.Validators.Create;
using EventShowcase.Infrastructure;
using FluentValidation;
using MediatR;


namespace EventShowcase.Application.UseCases.UserUseCases.Handlers.Register
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;


        public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var userEntity = _mapper.Map<User>(request);
            userEntity.PasswordHash = _passwordHasher.Generate(request.Password);

            await _userRepository.AddAsync(userEntity);

            return Unit.Value;
        }
    }
}

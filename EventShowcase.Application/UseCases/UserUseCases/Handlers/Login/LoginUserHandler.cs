using EventShowcase.API.Contracts.Users.Requests;
using EventShowcase.API.Contracts.Users.Responses;
using EventShowcase.Application.Exeptions;
using EventShowcase.Application.Interfaces.Auth;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Infrastructure;
using MediatR;


namespace EventShowcase.Application.UseCases.UserUseCases.Handlers.Login
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, (string, string)>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public LoginUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<(string, string)> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email)
                ?? throw new EntityNotFoundExeption("Entity not found");

            if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                throw new Exception("Invalid credentials");
            }

            var accessToken = _jwtProvider.GenerateAccessToken(user);
            var refreshToken = _jwtProvider.GenerateRefreshToken(user);
            return (accessToken, refreshToken);
        }
    }
}

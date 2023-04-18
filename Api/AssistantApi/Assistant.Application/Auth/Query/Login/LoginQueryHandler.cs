using Assistant.Application.Common.Interfaces;
using Assistant.Domain.Entities;
using MediatR;

namespace Assistant.Application.Auth.Query.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Guid>
    {
        public LoginQueryHandler(IAuthService authService)
        { 
            _authService = authService;
        }

        private readonly IAuthService _authService;

        public async Task<Guid> Handle(LoginQuery loginQuery, CancellationToken cancellationToken)
        {
            var isUserNew = await _authService.CheckUserAsync(loginQuery.Email);

            if (!isUserNew) throw new Exception("Пользователь с таким email уже существует");

            var userId = await _authService.CreateUserAsync(
                new User()
                {
                    Email = loginQuery.Email,
                    Password = loginQuery.Password,
                    Salt = "214313432",
                    FirstName = loginQuery.FirstName,
                    LastName = loginQuery.LastName,
                    Login = loginQuery.Login,
                    Right = 0,
                });

            return userId;
        }
    }
}

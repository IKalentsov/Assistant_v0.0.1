using MediatR;

namespace Assistant.Application.Auth.Query.Login
{
    public class LoginQuery : IRequest<string>
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}

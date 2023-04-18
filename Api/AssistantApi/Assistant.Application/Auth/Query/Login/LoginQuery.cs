using MediatR;

namespace Assistant.Application.Auth.Query.Login
{
    public class LoginQuery : IRequest<Guid>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }
    }
}

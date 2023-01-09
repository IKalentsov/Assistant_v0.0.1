using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistant.Application.Auth.Query.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        public LoginQueryHandler() { }

        public async Task<string> Handle(LoginQuery loginQuery, CancellationToken cancellationToken)
        {
            return string.Empty;
        }
    }
}

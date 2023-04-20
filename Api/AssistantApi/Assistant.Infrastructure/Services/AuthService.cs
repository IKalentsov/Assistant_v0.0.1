using Assistant.Application.Auth.Security;
using Assistant.Application.Common.Interfaces;
using Assistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assistant.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        public AuthService(IDatabaseContext databaseContext, IEncrypt encrypt) 
        {
            _databaseContext = databaseContext;
            _encrypt = encrypt;
        }

        private readonly IDatabaseContext _databaseContext;
        private readonly IEncrypt _encrypt;

        public async Task<Guid> CreateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            HashingPassword(user);

            _databaseContext.Users.Add(user);

            await _databaseContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }

        public async Task<bool> CheckUserAsync(string email)
        {
            var user = await _databaseContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user is null;
        }

        public async Task<User> GetUserAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = await _databaseContext.Users.Where(u => u.Email == email).Select(user => new User
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Salt = user.Salt,
                Right = user.Right
            }).FirstOrDefaultAsync(cancellationToken);
            
            return user ?? new User();
        }

        public async Task<User> GetUserAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _databaseContext.Users.Where(u => u.Id == id).Select(user => new User
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Salt = user.Salt,
                Right = user.Right
            }).FirstOrDefaultAsync(cancellationToken);

            return user ?? new User();
        }
    
        private void HashingPassword(User user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = _encrypt.HashPassword(user.Password, user.Salt);
        }
    }
}

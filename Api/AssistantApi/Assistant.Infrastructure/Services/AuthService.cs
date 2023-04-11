using Assistant.Application.Common.Interfaces;
using Assistant.Domain.Entities;
using Assistant.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assistant.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDatabaseContext _databaseContext;

        public AuthService(IDatabaseContext databaseContext) 
        {
            _databaseContext = databaseContext;
        }

        public async Task<Guid> CreateUser(UserModel userModel, CancellationToken cancellationToken = default)
        {
            var user = new User();

            user.Email = userModel.Email;
            user.Password = userModel.Password;
            user.Salt = userModel.Salt;
            user.Right = userModel.Right;

            _databaseContext.Users.Add(user);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }

        public async Task<UserModel> GetUser(string email, CancellationToken cancellationToken = default)
        {
            var user = await _databaseContext.Users.Where(u => u.Email == email).Select(user => new UserModel
            {
                UserId = user.Id,
                Email = user.Email,
                Password = user.Password,
                Salt = user.Salt,
                Right = user.Right
            }).FirstOrDefaultAsync(cancellationToken);

            if (user is null) throw new AuthenticationException("Пользователь не найден");

            return user;
        }

        public async Task<UserModel> GetUser(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _databaseContext.Users.Where(u => u.Id == id).Select(user => new UserModel
            {
                UserId = user.Id,
                Email = user.Email,
                Password = user.Password,
                Salt = user.Salt,
                Right = user.Right
            }).FirstOrDefaultAsync(cancellationToken);

            if (user is null) throw new AuthenticationException("Пользователь не найден");

            return user;
        }
    }
}

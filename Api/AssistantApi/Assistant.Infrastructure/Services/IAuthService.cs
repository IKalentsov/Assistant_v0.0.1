using Assistant.Infrastructure.Models;

namespace Assistant.Infrastructure.Services
{
    public interface IAuthService
    {
        Task<UserModel> GetUser(string email, CancellationToken cancellationToken = default);
        Task<UserModel> GetUser(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> CreateUser(UserModel userModel, CancellationToken cancellationToken = default);
    }
}

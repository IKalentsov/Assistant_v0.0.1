using Assistant.Domain.Entities;

namespace Assistant.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<User> GetUserAsync(string email, CancellationToken cancellationToken = default);
        Task<User> GetUserAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> CreateUserAsync(User userModel, CancellationToken cancellationToken = default);
        Task<bool> CheckUserAsync(string email);
    }
}

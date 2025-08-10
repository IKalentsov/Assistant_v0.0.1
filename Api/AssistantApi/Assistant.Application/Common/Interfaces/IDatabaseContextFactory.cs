namespace Assistant.Application.Common.Interfaces
{
    public interface IDatabaseContextFactory
    {
        Task<IDatabaseContext> CreateContextAsync(CancellationToken cancellationToken = default);
        IDatabaseContext CreateContext();
    }
}

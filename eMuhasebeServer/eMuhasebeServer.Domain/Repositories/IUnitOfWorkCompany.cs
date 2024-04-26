namespace eMuhasebeServer.Domain.Repositories;
public interface IUnitOfWorkCompany
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

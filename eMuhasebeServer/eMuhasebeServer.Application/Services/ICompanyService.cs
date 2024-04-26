using eMuhasebeServer.Domain.Entities;

namespace eMuhasebeServer.Application.Services;
public interface ICompanyService
{
    void MigrateAll(List<Company> companies);
}

namespace eMuhasebeServer.Domain.Entities;
public sealed class CompanyUser
{
    public Guid CompanyId { get; set; }
    public Company? Company { get; set; }
    public Guid UserId { get; set; }
}

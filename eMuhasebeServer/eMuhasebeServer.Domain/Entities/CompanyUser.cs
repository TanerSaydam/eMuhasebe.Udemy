namespace eMuhasebeServer.Domain.Entities;
public sealed class CompanyUser
{
    public Guid CompanyId { get; set; }
    public Company? Company { get; set; }
    public Guid AppUserId { get; set; }
}

using eMuhasebeServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMuhasebeServer.Infrastructure.Configurations;
internal sealed class CompanyUserConfiguration : IEntityTypeConfiguration<CompanyUser>
{
    public void Configure(EntityTypeBuilder<CompanyUser> builder)
    {
        builder.HasKey(x => new { x.AppUserId, x.CompanyId });
        builder.HasQueryFilter(filter => !filter.Company!.IsDeleted);        
    }
}

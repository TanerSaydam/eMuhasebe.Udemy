using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities;
public sealed class Product : Entity
{
    public string Name { get; set; } = string.Empty;
}

using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities;
public sealed class Product : Entity
{
    public string Name { get; set; } = string.Empty;
    public decimal Deposit {  get; set; }
    public decimal Withdrawal { get; set; }
    public List<ProductDetail>? Details { get; set; }
}

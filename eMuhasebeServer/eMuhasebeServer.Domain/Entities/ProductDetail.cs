using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities;
public sealed class ProductDetail : Entity
{
    public Guid ProductId { get; set; }
    public DateOnly Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Deposit { get; set; }
    public decimal Withdrawal { get; set; }
    public decimal Price { get; set; }
    public Guid? InvoiceId { get; set; }
}

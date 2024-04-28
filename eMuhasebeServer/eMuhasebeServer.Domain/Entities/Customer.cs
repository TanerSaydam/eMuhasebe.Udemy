using eMuhasebeServer.Domain.Abstractions;
using eMuhasebeServer.Domain.Enums;

namespace eMuhasebeServer.Domain.Entities;
public sealed class Customer : Entity
{
    public string Name { get; set; } = string.Empty;
    public CustomerTypeEnum Type { get; set; } = CustomerTypeEnum.Alicilar;
    public string City { get; set; } = string.Empty;
    public string Town { get; set; } = string.Empty;
    public string FullAddress { get; set; } = string.Empty;
    public string TaxDepartment { get; set; } = string.Empty;
    public string TaxNumber { get; set; } = string.Empty;
    public decimal DepositAmount { get; set; }
    public decimal WithdrawalAmount { get; set; }
}

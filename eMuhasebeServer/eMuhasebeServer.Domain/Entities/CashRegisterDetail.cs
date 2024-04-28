using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities;
public sealed class CashRegisterDetail : Entity
{
    public Guid CashRegisterId { get; set; }    
    public DateOnly Date {  get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal DepositAmount { get; set; } //Giriş
    public decimal WithdrawalAmount { get; set; } //Çıkış

    public Guid? CashRegisterDetailOppositeId { get; set; }
    //public CashRegisterDetail? CashRegisterDetailOpposite { get; set; }

}

using Ardalis.SmartEnum;

namespace eMuhasebeServer.Domain.Enums;
public sealed class CustomerTypeEnum : SmartEnum<CustomerTypeEnum>
{
    public static readonly CustomerTypeEnum Alicilar = new("Ticari Alıcılar", 1);
    public static readonly CustomerTypeEnum Saticilar = new("Ticari Satıcılar", 2);
    public static readonly CustomerTypeEnum Personel = new("Personel", 3);
    public static readonly CustomerTypeEnum Ortaklar = new("Şirket Ortakları", 3);
    public CustomerTypeEnum(string name, int value) : base(name, value)
    {
    }
}

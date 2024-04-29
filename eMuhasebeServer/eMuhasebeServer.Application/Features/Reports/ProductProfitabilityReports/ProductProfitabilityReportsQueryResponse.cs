namespace eMuhasebeServer.Application.Features.Reports.ProductProfitabilityReports;

public sealed record ProductProfitabilityReportsQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal DepositPrice { get; set; }
    public decimal WithdrawalPrice { get; set; }
    public decimal ProfitPercent { get; set; }
    public decimal Profit { get; set; }
}
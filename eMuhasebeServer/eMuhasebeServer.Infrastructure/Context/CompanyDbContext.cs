using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eMuhasebeServer.Infrastructure.Context;
internal sealed class CompanyDbContext : DbContext, IUnitOfWorkCompany
{
    #region Connection
    private string connectionString = string.Empty;

    public CompanyDbContext(Company company)
    {
        CreateConnectionStringWithCompany(company);
    }

    public CompanyDbContext(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
    {
        CreateConnectionString(httpContextAccessor, context);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

    private void CreateConnectionString(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
    {
        if (httpContextAccessor.HttpContext is null) return;

        string? companyId = httpContextAccessor.HttpContext.User.FindFirstValue("CompanyId");
        if (string.IsNullOrEmpty(companyId)) return;

        Company? company = context.Companies.Find(Guid.Parse(companyId));
        if (company is null) return;

        CreateConnectionStringWithCompany(company);
    }

    private void CreateConnectionStringWithCompany(Company company)
    {
        if (string.IsNullOrEmpty(company.Database.UserId))
        {
            connectionString =
            $"Data Source={company.Database.Server};" +
            $"Initial Catalog={company.Database.DatabaseName};" +
            "Integrated Security=True;" +
            "Connect Timeout=30;" +
            "Encrypt=True;" +
            "Trust Server Certificate=True;" +
            "Application Intent=ReadWrite;" +
            "Multi Subnet Failover=False";
        }
        else
        {
            connectionString =
            $"Data Source={company.Database.Server};" +
            $"Initial Catalog={company.Database.DatabaseName};" +
            "Integrated Security=False;" +
            $"User Id={company.Database.UserId};" +
            $"Password={company.Database.Password};" +
            "Connect Timeout=30;" +
            "Encrypt=True;" +
            "Trust Server Certificate=True;" +
            "Application Intent=ReadWrite;" +
            "Multi Subnet Failover=False";
        }
    }
    #endregion

    public DbSet<CashRegister> CashRegisters { get; set; }
    public DbSet<CashRegisterDetail> CashRegisterDetails { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<BankDetail> BankDetails { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerDetail> CustomerDetails { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductDetail> ProductDetail { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region CashRegister
        modelBuilder.Entity<CashRegister>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder.Entity<CashRegister>().Property(p => p.WithdrawalAmount).HasColumnType("money");
        modelBuilder.Entity<CashRegister>()
            .Property(p => p.CurrencyType)
            .HasConversion(type => type.Value, value => CurrencyTypeEnum.FromValue(value));
        modelBuilder.Entity<CashRegister>().HasQueryFilter(filter => !filter.IsDeleted);
        modelBuilder.Entity<CashRegister>()
            .HasMany(p => p.Details)
            .WithOne()
            .HasForeignKey(p => p.CashRegisterId);
        #endregion

        #region CashRegisterDetail
        modelBuilder.Entity<CashRegisterDetail>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder.Entity<CashRegisterDetail>().Property(p => p.WithdrawalAmount).HasColumnType("money");        
        #endregion

        #region Bank
        modelBuilder.Entity<Bank>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder.Entity<Bank>().Property(p => p.WithdrawalAmount).HasColumnType("money");
        modelBuilder.Entity<Bank>()
            .Property(p => p.CurrencyType)
            .HasConversion(type => type.Value, value => CurrencyTypeEnum.FromValue(value));
        modelBuilder.Entity<Bank>().HasQueryFilter(filter => !filter.IsDeleted);
        modelBuilder.Entity<Bank>()
           .HasMany(p => p.Details)
           .WithOne()
           .HasForeignKey(p => p.BankId);
        #endregion

        #region BankDetail
        modelBuilder.Entity<BankDetail>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder.Entity<BankDetail>().Property(p => p.WithdrawalAmount).HasColumnType("money");        
        #endregion

        #region Customer
        modelBuilder.Entity<Customer>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder.Entity<Customer>().Property(p => p.WithdrawalAmount).HasColumnType("money");
        modelBuilder.Entity<Customer>().Property(p => p.Type)
            .HasConversion(type => type.Value, value => CustomerTypeEnum.FromValue(value));
        modelBuilder.Entity<Customer>().HasQueryFilter(filter => !filter.IsDeleted);
        #endregion

        #region CustomerDetail
        modelBuilder.Entity<CustomerDetail>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder.Entity<CustomerDetail>().Property(p => p.WithdrawalAmount).HasColumnType("money");
        modelBuilder.Entity<CustomerDetail>().Property(p => p.Type)
           .HasConversion(type => type.Value, value => CustomerDetailTypeEnum.FromValue(value));
        #endregion

        #region ProductDetail
        modelBuilder.Entity<ProductDetail>().Property(p => p.Deposit).HasColumnType("decimal(7,2)");
        modelBuilder.Entity<ProductDetail>().Property(p => p.Withdrawal).HasColumnType("decimal(7,2)");
        modelBuilder.Entity<ProductDetail>().Property(p => p.Price).HasColumnType("money");
        #endregion

        #region Product
        modelBuilder.Entity<Product>().HasQueryFilter(filter => !filter.IsDeleted);
        modelBuilder.Entity<Product>().Property(p => p.Deposit).HasColumnType("decimal(7,2)");
        modelBuilder.Entity<Product>().Property(p => p.Withdrawal).HasColumnType("decimal(7,2)");
        #endregion

        #region Invoice
        modelBuilder.Entity<Invoice>().Property(p => p.Amount).HasColumnType("money");
        modelBuilder.Entity<Invoice>().Property(p => p.Type)
            .HasConversion(type => type.Value, value => InvoiceTypeEnum.FromValue(value));
        modelBuilder.Entity<Invoice>().HasQueryFilter(filter => !filter.IsDeleted);
        modelBuilder.Entity<Invoice>().HasQueryFilter(filter => !filter.Customer!.IsDeleted);        
        #endregion

        #region InvoiceDetail
        modelBuilder.Entity<InvoiceDetail>().Property(p => p.Quantity).HasColumnType("decimal(7,2)");
        modelBuilder.Entity<InvoiceDetail>().Property(p => p.Price).HasColumnType("money");
        modelBuilder.Entity<InvoiceDetail>().HasQueryFilter(filter => !filter.Product!.IsDeleted);
        #endregion

    }
}

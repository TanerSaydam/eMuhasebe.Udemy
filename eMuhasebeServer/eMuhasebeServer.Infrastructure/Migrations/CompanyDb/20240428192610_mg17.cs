using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eMuhasebeServer.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class mg17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Customers",
                newName: "FullAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullAddress",
                table: "Customers",
                newName: "Address");
        }
    }
}

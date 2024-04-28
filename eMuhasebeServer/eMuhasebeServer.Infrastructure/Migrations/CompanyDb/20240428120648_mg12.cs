using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eMuhasebeServer.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class mg12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankDetails_BankDetails_BankDetailOppositeId",
                table: "BankDetails");

            migrationBuilder.DropIndex(
                name: "IX_BankDetails_BankDetailOppositeId",
                table: "BankDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BankDetails_BankDetailOppositeId",
                table: "BankDetails",
                column: "BankDetailOppositeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankDetails_BankDetails_BankDetailOppositeId",
                table: "BankDetails",
                column: "BankDetailOppositeId",
                principalTable: "BankDetails",
                principalColumn: "Id");
        }
    }
}

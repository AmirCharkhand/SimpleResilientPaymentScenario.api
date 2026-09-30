using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleResilientPaymentScenario.api.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueToOrderId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Payment_OrderId",
                table: "Payment",
                column: "OrderId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payment_OrderId",
                table: "Payment");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyPlus.Migrations
{
    /// <inheritdoc />
    public partial class NemPropClassTransfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountIdReceived",
                table: "Transfers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountIdReceived",
                table: "Transfers");
        }
    }
}

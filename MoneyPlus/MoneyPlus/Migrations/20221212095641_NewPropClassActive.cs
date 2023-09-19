using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyPlus.Migrations
{
    /// <inheritdoc />
    public partial class NewPropClassActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "Actives",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Actives");
        }
    }
}

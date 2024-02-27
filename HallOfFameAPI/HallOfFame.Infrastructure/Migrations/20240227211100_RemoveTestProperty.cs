using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HallOfFame.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTestProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                table: "Skills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Test",
                table: "Skills",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

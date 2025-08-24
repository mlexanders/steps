using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Steps.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class _123123123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasScore",
                table: "FinalScheduledCell",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasScore",
                table: "FinalScheduledCell");
        }
    }
}

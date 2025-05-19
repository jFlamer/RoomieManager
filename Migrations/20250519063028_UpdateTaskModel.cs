using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomieManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTaskModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "roommieID",
                table: "Tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "roommieID",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}

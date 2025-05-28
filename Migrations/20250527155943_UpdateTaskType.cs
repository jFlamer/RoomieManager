using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomieManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTaskType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "effortPoints",
                table: "TaskTypes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "effortPoints",
                table: "TaskTypes");
        }
    }
}

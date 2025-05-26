using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomieManager.Migrations
{
    /// <inheritdoc />
    public partial class changedTaskModelConstr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Roomies_roomieID",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "roomieID",
                table: "Tasks",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Roomies_roomieID",
                table: "Tasks",
                column: "roomieID",
                principalTable: "Roomies",
                principalColumn: "roomieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Roomies_roomieID",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "roomieID",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Roomies_roomieID",
                table: "Tasks",
                column: "roomieID",
                principalTable: "Roomies",
                principalColumn: "roomieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomieManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskTypes",
                columns: table => new
                {
                    taskTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    duration = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypes", x => x.taskTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userName = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    isAdmin = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Roomies",
                columns: table => new
                {
                    roomieId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userId = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    photoURL = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roomies", x => x.roomieId);
                    table.ForeignKey(
                        name: "FK_Roomies_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    typeID = table.Column<int>(type: "INTEGER", nullable: false),
                    roommieID = table.Column<int>(type: "INTEGER", nullable: false),
                    priority = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => new { x.typeID, x.roommieID });
                    table.ForeignKey(
                        name: "FK_Priorities_Roomies_roommieID",
                        column: x => x.roommieID,
                        principalTable: "Roomies",
                        principalColumn: "roomieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Priorities_TaskTypes_typeID",
                        column: x => x.typeID,
                        principalTable: "TaskTypes",
                        principalColumn: "taskTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    taskID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    typeID = table.Column<int>(type: "INTEGER", nullable: false),
                    roommieID = table.Column<int>(type: "INTEGER", nullable: false),
                    roomieID = table.Column<int>(type: "INTEGER", nullable: false),
                    plannedStartDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    plannedFinishDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    reviewDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    reviewNote = table.Column<string>(type: "TEXT", nullable: false),
                    reviewRoomieID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.taskID);
                    table.ForeignKey(
                        name: "FK_Tasks_Roomies_reviewRoomieID",
                        column: x => x.reviewRoomieID,
                        principalTable: "Roomies",
                        principalColumn: "roomieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Roomies_roomieID",
                        column: x => x.roomieID,
                        principalTable: "Roomies",
                        principalColumn: "roomieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskTypes_typeID",
                        column: x => x.typeID,
                        principalTable: "TaskTypes",
                        principalColumn: "taskTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Priorities_roommieID",
                table: "Priorities",
                column: "roommieID");

            migrationBuilder.CreateIndex(
                name: "IX_Roomies_userId",
                table: "Roomies",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_reviewRoomieID",
                table: "Tasks",
                column: "reviewRoomieID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_roomieID",
                table: "Tasks",
                column: "roomieID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_typeID",
                table: "Tasks",
                column: "typeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Roomies");

            migrationBuilder.DropTable(
                name: "TaskTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

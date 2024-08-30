using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Light.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleItems_Schedules_ScheduleId",
                table: "ScheduleItems");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "ScheduleItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleItems_Schedules_ScheduleId",
                table: "ScheduleItems",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleItems_Schedules_ScheduleId",
                table: "ScheduleItems");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "ScheduleItems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleItems_Schedules_ScheduleId",
                table: "ScheduleItems",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");
        }
    }
}

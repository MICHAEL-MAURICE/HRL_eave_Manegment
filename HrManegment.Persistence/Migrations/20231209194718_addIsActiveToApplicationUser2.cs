using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addIsActiveToApplicationUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: new Guid("6a6322bc-184d-4a87-9898-96af9c44a87a"));

            migrationBuilder.AddColumn<Guid>(
                name: "LeaveTypeId",
                table: "LeaveRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5727a3d-4df6-4c04-9b2e-12d078a50bf2", "AQAAAAIAAYagAAAAEPs0IgevwiElh/ImAOqGPx/yi8AFVIVh+y0vutLKxH8WawpKzW4hi1cgh0Zd/T/leg==", "7d798622-361c-40c5-8c58-f2c41313efcb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e5ecd16-daab-4169-96e9-3327cd721ea8", "AQAAAAIAAYagAAAAEG76DeUnqgwDlmO6nMKN6XUfUzjsDrel5JUqPQny53YgDl7QT6jw0589aE0VVw9lSQ==", "cf54aa99-6ede-4e43-9d88-d3e189e5dd82" });

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "DefaultDays", "ModifiedBy", "Name" },
                values: new object[] { new Guid("fb12b38f-5869-471d-b86a-42805df5e640"), null, new DateTime(2023, 12, 9, 21, 47, 18, 773, DateTimeKind.Local).AddTicks(9190), new DateTime(2023, 12, 9, 21, 47, 18, 773, DateTimeKind.Local).AddTicks(9257), 10, null, "Vacation" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                table: "LeaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                table: "LeaveRequests");

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: new Guid("fb12b38f-5869-471d-b86a-42805df5e640"));

            migrationBuilder.DropColumn(
                name: "LeaveTypeId",
                table: "LeaveRequests");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "791c2dd6-0868-4829-a2d6-fa0a335cdb8d", "AQAAAAIAAYagAAAAEE2Gntw4w5q+p95Bw7R1CT61uw1AXAvvjc7Yw063Egz0DfKk/Obt2boPudkHzg/zZQ==", "abc20f43-0acf-4d12-a1c0-cea7f7f120f8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61dac0fe-7778-4561-a3d9-61c17cd510c3", "AQAAAAIAAYagAAAAEPsfS60mx5NBp2S+PEJbdYgNzVoNk1K2LDuMy0+SuKWXEkGw9lF4Km4l4GcHNpETbg==", "4b6fc909-3fa6-4d66-95b5-21bbf550e397" });

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "DefaultDays", "ModifiedBy", "Name" },
                values: new object[] { new Guid("6a6322bc-184d-4a87-9898-96af9c44a87a"), null, new DateTime(2023, 12, 9, 21, 46, 21, 359, DateTimeKind.Local).AddTicks(168), new DateTime(2023, 12, 9, 21, 46, 21, 359, DateTimeKind.Local).AddTicks(229), 10, null, "Vacation" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addIsActiveToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId1",
                table: "LeaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequests_LeaveTypeId1",
                table: "LeaveRequests");

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: new Guid("202aa598-33e0-451b-901d-8d9c2c408685"));

            migrationBuilder.DropColumn(
                name: "LeaveTypeId",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "LeaveTypeId1",
                table: "LeaveRequests");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash", "SecurityStamp" },
                values: new object[] { "791c2dd6-0868-4829-a2d6-fa0a335cdb8d", null, "AQAAAAIAAYagAAAAEE2Gntw4w5q+p95Bw7R1CT61uw1AXAvvjc7Yw063Egz0DfKk/Obt2boPudkHzg/zZQ==", "abc20f43-0acf-4d12-a1c0-cea7f7f120f8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61dac0fe-7778-4561-a3d9-61c17cd510c3", null, "AQAAAAIAAYagAAAAEPsfS60mx5NBp2S+PEJbdYgNzVoNk1K2LDuMy0+SuKWXEkGw9lF4Km4l4GcHNpETbg==", "4b6fc909-3fa6-4d66-95b5-21bbf550e397" });

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "DefaultDays", "ModifiedBy", "Name" },
                values: new object[] { new Guid("6a6322bc-184d-4a87-9898-96af9c44a87a"), null, new DateTime(2023, 12, 9, 21, 46, 21, 359, DateTimeKind.Local).AddTicks(168), new DateTime(2023, 12, 9, 21, 46, 21, 359, DateTimeKind.Local).AddTicks(229), 10, null, "Vacation" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: new Guid("6a6322bc-184d-4a87-9898-96af9c44a87a"));

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "LeaveTypeId",
                table: "LeaveRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "LeaveTypeId1",
                table: "LeaveRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9423255c-b2b3-4945-a082-3004c425719c", "AQAAAAIAAYagAAAAEM5BzBAWKzHufpQZGrA0a0sDnE3RQUEJoqSibxypQUZthge86Z0mHxNV27CSWX7uFg==", "2e2d3c81-e4c5-4a49-9825-4a7cd0f97876" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a09b3eff-9aa6-42c5-bc93-7d987219b3f4", "AQAAAAIAAYagAAAAEC5Z5qnjd69BI7w0gHwHRBIlIef09k8cYcZ0OKAR/QTP/E0k/Qi2p+hA1VdLu7sQ1Q==", "96db9f63-4d5e-4467-82ec-d6864cf5a475" });

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "DefaultDays", "ModifiedBy", "Name" },
                values: new object[] { new Guid("202aa598-33e0-451b-901d-8d9c2c408685"), null, new DateTime(2023, 11, 29, 16, 9, 38, 942, DateTimeKind.Local).AddTicks(5894), new DateTime(2023, 11, 29, 16, 9, 38, 942, DateTimeKind.Local).AddTicks(6043), 10, null, "Vacation" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeId1",
                table: "LeaveRequests",
                column: "LeaveTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId1",
                table: "LeaveRequests",
                column: "LeaveTypeId1",
                principalTable: "LeaveTypes",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityProvider.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoleSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 8, 0, 34, 534, DateTimeKind.Utc).AddTicks(4363));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 8, 0, 34, 534, DateTimeKind.Utc).AddTicks(4365));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 2, 19, 8, 0, 34, 534, DateTimeKind.Utc).AddTicks(4175), "Admin" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 2, 19, 8, 0, 34, 534, DateTimeKind.Utc).AddTicks(4170), "User" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 8, 0, 34, 534, DateTimeKind.Utc).AddTicks(4337));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 10, 26, 57, 37, DateTimeKind.Utc).AddTicks(2965));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 10, 26, 57, 37, DateTimeKind.Utc).AddTicks(2968));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 2, 18, 10, 26, 57, 37, DateTimeKind.Utc).AddTicks(2791), "User" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2026, 2, 18, 10, 26, 57, 37, DateTimeKind.Utc).AddTicks(2793), "Admin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 10, 26, 57, 37, DateTimeKind.Utc).AddTicks(2942));
        }
    }
}

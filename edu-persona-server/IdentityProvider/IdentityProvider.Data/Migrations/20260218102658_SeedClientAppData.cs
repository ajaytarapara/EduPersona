using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityProvider.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedClientAppData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "AppName", "ClientSecret", "CreatedAt", "IsActive", "PostLogoutUris", "RedirectUris", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "User Profile App", "PIY135_USER_PROFILE_APP_531yip", new DateTime(2026, 2, 18, 10, 26, 57, 37, DateTimeKind.Utc).AddTicks(2965), true, "https://profile.app/logout", "https://profile.app/callback", null },
                    { 2, "Exam App", "ADG086_EXAM_APP_680adg", new DateTime(2026, 2, 18, 10, 26, 57, 37, DateTimeKind.Utc).AddTicks(2968), true, "https://exam.app/logout", "https://exam.app/callback", null }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 10, 26, 57, 37, DateTimeKind.Utc).AddTicks(2791));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 18, 10, 26, 57, 37, DateTimeKind.Utc).AddTicks(2793));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 2, 18, 10, 26, 57, 37, DateTimeKind.Utc).AddTicks(2942), "6G94qKPK8LYNjnTllCqm2G3BUM08AzOK7yW30tfjrMc=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 11, 32, 2, 156, DateTimeKind.Utc).AddTicks(2251));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 17, 11, 32, 2, 156, DateTimeKind.Utc).AddTicks(2256));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 2, 17, 11, 32, 2, 156, DateTimeKind.Utc).AddTicks(2379), "Admin@123" });
        }
    }
}

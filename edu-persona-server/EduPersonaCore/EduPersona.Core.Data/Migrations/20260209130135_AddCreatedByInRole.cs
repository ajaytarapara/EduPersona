using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduPersona.Core.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByInRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy", "DeletedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 2, 9, 13, 1, 34, 712, DateTimeKind.Unspecified).AddTicks(6703), new TimeSpan(0, 0, 0, 0, 0)), 1, new DateTimeOffset(new DateTime(2026, 2, 9, 13, 1, 34, 712, DateTimeKind.Unspecified).AddTicks(6707), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 2, 9, 13, 1, 34, 712, DateTimeKind.Unspecified).AddTicks(6706), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "CreatedBy", "DeletedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 2, 9, 13, 1, 34, 712, DateTimeKind.Unspecified).AddTicks(6708), new TimeSpan(0, 0, 0, 0, 0)), 1, new DateTimeOffset(new DateTime(2026, 2, 9, 13, 1, 34, 712, DateTimeKind.Unspecified).AddTicks(6709), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 2, 9, 13, 1, 34, 712, DateTimeKind.Unspecified).AddTicks(6709), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DeletedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 2, 9, 13, 1, 34, 712, DateTimeKind.Unspecified).AddTicks(6917), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 2, 9, 13, 1, 34, 712, DateTimeKind.Unspecified).AddTicks(6918), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 2, 9, 13, 1, 34, 712, DateTimeKind.Unspecified).AddTicks(6917), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy", "DeletedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 2, 9, 12, 59, 50, 566, DateTimeKind.Unspecified).AddTicks(686), new TimeSpan(0, 0, 0, 0, 0)), 0, new DateTimeOffset(new DateTime(2026, 2, 9, 12, 59, 50, 566, DateTimeKind.Unspecified).AddTicks(689), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 2, 9, 12, 59, 50, 566, DateTimeKind.Unspecified).AddTicks(689), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "CreatedBy", "DeletedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 2, 9, 12, 59, 50, 566, DateTimeKind.Unspecified).AddTicks(691), new TimeSpan(0, 0, 0, 0, 0)), 0, new DateTimeOffset(new DateTime(2026, 2, 9, 12, 59, 50, 566, DateTimeKind.Unspecified).AddTicks(692), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 2, 9, 12, 59, 50, 566, DateTimeKind.Unspecified).AddTicks(692), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DeletedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 2, 9, 12, 59, 50, 566, DateTimeKind.Unspecified).AddTicks(966), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 2, 9, 12, 59, 50, 566, DateTimeKind.Unspecified).AddTicks(966), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 2, 9, 12, 59, 50, 566, DateTimeKind.Unspecified).AddTicks(966), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}

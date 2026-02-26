using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduPersona.Core.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnInUserDesignation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrent",
                table: "UserDesignations");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "UserProfile",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "ProfessionId",
                table: "UserDesignations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserDesignations_ProfessionId",
                table: "UserDesignations",
                column: "ProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDesignations_Professions_ProfessionId",
                table: "UserDesignations",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDesignations_Professions_ProfessionId",
                table: "UserDesignations");

            migrationBuilder.DropIndex(
                name: "IX_UserDesignations_ProfessionId",
                table: "UserDesignations");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "UserDesignations");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserProfile",
                newName: "userId");

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrent",
                table: "UserDesignations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduPersona.Core.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUdUs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserSkills",
                newName: "UserProfileId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserDesignations",
                newName: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_UserProfileId",
                table: "UserSkills",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDesignations_UserProfileId",
                table: "UserDesignations",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDesignations_UserProfile_UserProfileId",
                table: "UserDesignations",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkills_UserProfile_UserProfileId",
                table: "UserSkills",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDesignations_UserProfile_UserProfileId",
                table: "UserDesignations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkills_UserProfile_UserProfileId",
                table: "UserSkills");

            migrationBuilder.DropIndex(
                name: "IX_UserSkills_UserProfileId",
                table: "UserSkills");

            migrationBuilder.DropIndex(
                name: "IX_UserDesignations_UserProfileId",
                table: "UserDesignations");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "UserSkills",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "UserDesignations",
                newName: "UserId");
        }
    }
}

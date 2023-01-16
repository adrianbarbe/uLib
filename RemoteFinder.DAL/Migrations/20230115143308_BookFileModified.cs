using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RemoteFinder.DAL.Migrations
{
    public partial class BookFileModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserSocialId",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserSocialId",
                table: "Categories",
                column: "UserSocialId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_FileId",
                table: "Books",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Files_FileId",
                table: "Books",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_UsersSocial_UserSocialId",
                table: "Categories",
                column: "UserSocialId",
                principalTable: "UsersSocial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Files_FileId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_UsersSocial_UserSocialId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UserSocialId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Books_FileId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UserSocialId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Books");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddUserLikeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Users_UserID",
                table: "Photos");

            migrationBuilder.CreateTable(
                name: "UserLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceUserId = table.Column<int>(type: "int", nullable: false),
                    LikedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLikes_Users_LikedUserId",
                        column: x => x.LikedUserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserLikes_Users_SourceUserId",
                        column: x => x.SourceUserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLikes_LikedUserId",
                table: "UserLikes",
                column: "LikedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikes_SourceUserId",
                table: "UserLikes",
                column: "SourceUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Users_UserID",
                table: "Photos",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Users_UserID",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "UserLikes");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Users_UserID",
                table: "Photos",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}

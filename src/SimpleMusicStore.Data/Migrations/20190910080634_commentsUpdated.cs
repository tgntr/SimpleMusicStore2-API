using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleMusicStore.Data.Migrations
{
    public partial class commentsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordComments");

            migrationBuilder.AddColumn<int>(
                name: "RecordId",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RecordId",
                table: "Comments",
                column: "RecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Records_RecordId",
                table: "Comments",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Records_RecordId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_RecordId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "Comments");

            migrationBuilder.CreateTable(
                name: "RecordComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommentId = table.Column<int>(nullable: false),
                    RecordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordComments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordComments_Records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordComments_CommentId",
                table: "RecordComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordComments_RecordId",
                table: "RecordComments",
                column: "RecordId");
        }
    }
}

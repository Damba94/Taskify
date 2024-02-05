using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTagBoardRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoardId",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_BoardId",
                table: "Tags",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Boards_BoardId",
                table: "Tags",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Boards_BoardId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_BoardId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Tags");
        }
    }
}

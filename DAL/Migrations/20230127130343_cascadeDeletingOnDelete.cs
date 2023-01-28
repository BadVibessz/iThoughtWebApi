using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class cascadeDeletingOnDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Diaries_DiaryId",
                schema: "public",
                table: "Notes");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Diaries_DiaryId",
                schema: "public",
                table: "Notes",
                column: "DiaryId",
                principalSchema: "public",
                principalTable: "Diaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Diaries_DiaryId",
                schema: "public",
                table: "Notes");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Diaries_DiaryId",
                schema: "public",
                table: "Notes",
                column: "DiaryId",
                principalSchema: "public",
                principalTable: "Diaries",
                principalColumn: "Id");
        }
    }
}

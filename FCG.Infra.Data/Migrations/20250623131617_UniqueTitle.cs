using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCG.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UniqueTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_games_Title",
                table: "games",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_games_Title",
                table: "games");
        }
    }
}

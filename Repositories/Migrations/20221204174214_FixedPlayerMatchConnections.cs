using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class FixedPlayerMatchConnections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PlayerMatches_MatchId",
                table: "PlayerMatches",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerMatches_Matches_MatchId",
                table: "PlayerMatches",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerMatches_Players_PlayerId",
                table: "PlayerMatches",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerMatches_Matches_MatchId",
                table: "PlayerMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerMatches_Players_PlayerId",
                table: "PlayerMatches");

            migrationBuilder.DropIndex(
                name: "IX_PlayerMatches_MatchId",
                table: "PlayerMatches");
        }
    }
}

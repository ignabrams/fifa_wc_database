using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class CorrectingMatchesPlayed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchedPlayed",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "MatchesPlayed",
                table: "Teams",
                type: "int",
                nullable: false,
                computedColumnSql: "([Wins] + [Losses] + [Draws])");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchesPlayed",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "MatchedPlayed",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

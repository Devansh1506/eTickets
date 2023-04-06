using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eTickets.Migrations
{
    /// <inheritdoc />
    public partial class Movie_date_change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageURl",
                table: "Movies",
                newName: "ImageURL");

            migrationBuilder.RenameColumn(
                name: "StratDate",
                table: "Movies",
                newName: "StartDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "Movies",
                newName: "ImageURl");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Movies",
                newName: "StratDate");
        }
    }
}

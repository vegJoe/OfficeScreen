using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeScreen.Migrations
{
    /// <inheritdoc />
    public partial class NewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "webcomics");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "webcomics",
                newName: "Source");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Source",
                table: "webcomics",
                newName: "Url");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "webcomics",
                type: "TEXT",
                nullable: true);
        }
    }
}

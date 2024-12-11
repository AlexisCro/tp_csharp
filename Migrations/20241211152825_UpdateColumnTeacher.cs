using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tpnote.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fistname",
                table: "Teachers",
                newName: "Firstname");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "Teachers",
                newName: "Fistname");
        }
    }
}

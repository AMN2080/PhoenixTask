using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoenixTask.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addAuthandPasswordChangeToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthKey",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsChangePassword",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthKey",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsChangePassword",
                table: "User");
        }
    }
}

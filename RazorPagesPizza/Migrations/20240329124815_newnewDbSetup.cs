using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesPizza.Migrations
{
    /// <inheritdoc />
    public partial class newnewDbSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Uid",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uid",
                table: "AspNetUsers");
        }
    }
}

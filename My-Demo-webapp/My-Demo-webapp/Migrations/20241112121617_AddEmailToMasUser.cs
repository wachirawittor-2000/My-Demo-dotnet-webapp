using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Demo_webapp.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailToMasUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_YourEntities",
                table: "YourEntities");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "MasUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MasUsers",
                table: "MasUsers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "MasUsers");

            migrationBuilder.RenameTable(
                name: "MasUsers",
                newName: "YourEntities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_YourEntities",
                table: "YourEntities",
                column: "Id");
        }
    }
}

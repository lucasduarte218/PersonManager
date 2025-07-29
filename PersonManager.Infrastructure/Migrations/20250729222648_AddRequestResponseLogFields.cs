using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestResponseLogFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "RequestResponseLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogType",
                table: "RequestResponseLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestHeaders",
                table: "RequestResponseLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponseHeaders",
                table: "RequestResponseLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "RequestResponseLogs");

            migrationBuilder.DropColumn(
                name: "LogType",
                table: "RequestResponseLogs");

            migrationBuilder.DropColumn(
                name: "RequestHeaders",
                table: "RequestResponseLogs");

            migrationBuilder.DropColumn(
                name: "ResponseHeaders",
                table: "RequestResponseLogs");
        }
    }
}

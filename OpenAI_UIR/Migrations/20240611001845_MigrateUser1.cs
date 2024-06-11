using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAI_UIR.Migrations
{
    /// <inheritdoc />
    public partial class MigrateUser1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("bcf486ca-e20c-4995-b9a4-0fb98ac685ca"));

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "CreatedAt", "Name", "Password", "UserName" },
                values: new object[] { new Guid("d1d58ddc-6a84-490a-97d1-874e54a414a5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jobintech", "@Jobintech2024@", "jobintech@jobintech-uir.ma" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("d1d58ddc-6a84-490a-97d1-874e54a414a5"));

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "CreatedAt", "Name", "Password", "UserName" },
                values: new object[] { new Guid("bcf486ca-e20c-4995-b9a4-0fb98ac685ca"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jobintech", "@Jobintech2024@", "jobintech@jobintech-uir.ma" });
        }
    }
}

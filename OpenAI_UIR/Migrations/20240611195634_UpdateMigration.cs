using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAI_UIR.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("d1d58ddc-6a84-490a-97d1-874e54a414a5"));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "CreatedAt", "Name", "Password", "UserName" },
                values: new object[] { new Guid("88d0b2cd-c363-471b-8e72-e7bfd153fc70"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jobintech", "@Jobintech2024@", "jobintech@jobintech-uir.ma" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("88d0b2cd-c363-471b-8e72-e7bfd153fc70"));

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "CreatedAt", "Name", "Password", "UserName" },
                values: new object[] { new Guid("d1d58ddc-6a84-490a-97d1-874e54a414a5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jobintech", "@Jobintech2024@", "jobintech@jobintech-uir.ma" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoundVerse.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
#pragma warning disable IDE1006 // Naming Styles
    public partial class modifytables : Migration
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAtUtc",
                table: "Category",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Category",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAtUtc",
                table: "Book",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Book",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAtUtc",
                table: "Author",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Author",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Author");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOFA.Migrations
{
    /// <inheritdoc />
    public partial class CreateExhibitionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exhibitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Pic = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Starts = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ends = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exhibitions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exhibitions");
        }
    }
}

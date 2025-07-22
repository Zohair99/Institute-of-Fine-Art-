using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOFA.Migrations
{
    /// <inheritdoc />
    public partial class CreateAwardTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "uploadname",
                table: "C_Art",
                newName: "UploadName");

            migrationBuilder.RenameColumn(
                name: "uploademal",
                table: "C_Art",
                newName: "UploadEmal");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "C_Art",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "C_Art",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "C_Art",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "UploadName",
                table: "C_Art",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "UploadEmal",
                table: "C_Art",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "C_Art",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "C_Art",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "CompetitionId",
                table: "C_Art",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "C_Art",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Awards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Awards", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Awards");

            migrationBuilder.DropColumn(
                name: "CompetitionId",
                table: "C_Art");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "C_Art");

            migrationBuilder.RenameColumn(
                name: "UploadName",
                table: "C_Art",
                newName: "uploadname");

            migrationBuilder.RenameColumn(
                name: "UploadEmal",
                table: "C_Art",
                newName: "uploademal");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "C_Art",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "C_Art",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "C_Art",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "uploadname",
                table: "C_Art",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "uploademal",
                table: "C_Art",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "C_Art",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "C_Art",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

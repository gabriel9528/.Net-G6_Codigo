using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseFirst.Migrations
{
    /// <inheritdoc />
    public partial class table_newTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "TablaNueva",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablaNueva", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TablaNueva");

            migrationBuilder.DropColumn(
                name: "ColumnaNueva",
                table: "Cliente");

            migrationBuilder.AddColumn<bool>(
                name: "StatusMarrried",
                table: "Cliente",
                type: "bit",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseFirst.Migrations
{
    /// <inheritdoc />
    public partial class modified_cliente_gender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "Cliente",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Cliente");
        }
    }
}

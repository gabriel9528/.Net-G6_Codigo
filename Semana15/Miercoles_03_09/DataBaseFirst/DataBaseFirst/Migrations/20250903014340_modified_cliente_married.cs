using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseFirst.Migrations
{
    /// <inheritdoc />
    public partial class modified_cliente_married : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Cliente",
                newName: "descripcion");

            migrationBuilder.AlterColumn<string>(
                name: "Genero",
                table: "Cliente",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "descripcion",
                table: "Cliente",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AddColumn<bool>(
                name: "StatusMarrried",
                table: "Cliente",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "StatusMarrried",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "descripcion",
                table: "Cliente",
                newName: "Descripcion");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Cliente",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Genero",
                table: "Cliente",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldDefaultValue: "");
        }
    }
}

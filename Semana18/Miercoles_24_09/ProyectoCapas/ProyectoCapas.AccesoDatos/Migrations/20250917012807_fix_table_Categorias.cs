﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoCapas.Data.Migrations
{
    /// <inheritdoc />
    public partial class fix_table_Categorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Categorias",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Categorias");
        }
    }
}

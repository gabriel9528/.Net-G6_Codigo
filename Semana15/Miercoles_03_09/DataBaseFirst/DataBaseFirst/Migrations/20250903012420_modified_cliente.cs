using System;
using DataBaseFirst.Data;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseFirst.Migrations
{
    /// <inheritdoc />
    public partial class modified_cliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Cliente>("descripcion", "Cliente", "varchar(200)", nullable:true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoEFCoreRelationship.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessPerson_Persons_PeopleId",
                table: "BusinessPerson");

            migrationBuilder.RenameColumn(
                name: "PeopleId",
                table: "BusinessPerson",
                newName: "PersonsId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessPerson_PeopleId",
                table: "BusinessPerson",
                newName: "IX_BusinessPerson_PersonsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessPerson_Persons_PersonsId",
                table: "BusinessPerson",
                column: "PersonsId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessPerson_Persons_PersonsId",
                table: "BusinessPerson");

            migrationBuilder.RenameColumn(
                name: "PersonsId",
                table: "BusinessPerson",
                newName: "PeopleId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessPerson_PersonsId",
                table: "BusinessPerson",
                newName: "IX_BusinessPerson_PeopleId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessPerson_Persons_PeopleId",
                table: "BusinessPerson",
                column: "PeopleId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

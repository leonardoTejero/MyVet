using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructure.Core.Migrations
{
    public partial class addFieldObservationInTableDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                schema: "Security",
                table: "User",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Observation",
                schema: "Vet",
                table: "Dates",
                maxLength: 300,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observation",
                schema: "Vet",
                table: "Dates");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "Security",
                table: "User",
                newName: "name");
        }
    }
}

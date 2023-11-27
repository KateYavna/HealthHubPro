using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addPersonToHealthcareProvider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_HealthcareProviders_PersonId",
                table: "HealthcareProviders",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthcareProviders_Persons_PersonId",
                table: "HealthcareProviders",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthcareProviders_Persons_PersonId",
                table: "HealthcareProviders");

            migrationBuilder.DropIndex(
                name: "IX_HealthcareProviders_PersonId",
                table: "HealthcareProviders");
        }
    }
}

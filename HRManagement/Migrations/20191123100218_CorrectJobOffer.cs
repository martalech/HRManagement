using Microsoft.EntityFrameworkCore.Migrations;

namespace HRManagement.Migrations
{
    public partial class CorrectJobOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "JobOffers");

            migrationBuilder.AddColumn<int>(
                name: "CompanyNameId",
                table: "JobOffers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobOffers_CompanyNameId",
                table: "JobOffers",
                column: "CompanyNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffers_Companies_CompanyNameId",
                table: "JobOffers",
                column: "CompanyNameId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_Companies_CompanyNameId",
                table: "JobOffers");

            migrationBuilder.DropIndex(
                name: "IX_JobOffers_CompanyNameId",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "CompanyNameId",
                table: "JobOffers");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "JobOffers",
                nullable: true);
        }
    }
}

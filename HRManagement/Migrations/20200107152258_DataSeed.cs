using Microsoft.EntityFrameworkCore.Migrations;

namespace HRManagement.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Marta Company" });

            migrationBuilder.InsertData(
                table: "JobOffers",
                columns: new[] { "Id", "CompanyNameId", "ContractType", "Description", "JobTitle", "Location", "Salary" },
                values: new object[] { 1, 1, "Full-time", "Near metro", "C# Developer", "Warsaw", 10 });

            migrationBuilder.InsertData(
                table: "JobApplications",
                columns: new[] { "Id", "ContactAgreement", "CvUrl", "EmailAddress", "FirstName", "JobOfferId", "LastName", "PhoneNumber" },
                values: new object[] { 1, false, "https://example.com", "a@b.c", "Marta", 1, "Lech", "1421412" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JobApplications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobOffers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

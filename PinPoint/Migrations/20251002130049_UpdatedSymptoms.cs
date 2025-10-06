using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSymptoms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfDays",
                table: "Symptoms");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Symptoms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "81595e0c-7672-4966-af3a-6bb55c4e3946", "AQAAAAIAAYagAAAAECQx7kJwtH1sCsMIT9zo4wdKlQH86E5ZqKvOCL1q9UrqwAMLbaRTY+hRXwccO5XQSg==", "e7780e8b-2321-458e-84c3-70e1dc4dec0c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Symptoms");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDays",
                table: "Symptoms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f48670e-48e5-4ef8-925e-5ad260b4b186", "AQAAAAIAAYagAAAAED0WKkWTgUsJ8EC0An5J+wiiY1ylJ8s3dSt6b5vjOK7a6BSdJNeimoFJKkiEj2ReUQ==", "f9e9863a-ac1b-488c-82da-dfa3ed617117" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSymptomDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Symptoms");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11444810-9058-4f7a-90ad-fcec9dd22501", "AQAAAAIAAYagAAAAEJlrUrBwiVukHMW4AR3AdcaAamdzyyjqL8XHCFWolyA+sYhR1CF4qacamFfyYnVV0Q==", "9c411f2e-4fca-469a-8e28-2bed5260ff73" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}

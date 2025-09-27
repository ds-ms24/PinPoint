using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b8a44de3-054d-44f4-8d22-b6c7286ce135", null, "Patient", "PATIENT" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b2b9b9a-aa86-4ccf-a74e-07819886e52d", "AQAAAAIAAYagAAAAEIQcK4k/MJdTFyUciLs3VCTHyOfBGDA2Saj71txaOqU/7xH3e+tH5E4i4pie2i/c8g==", "803dd53f-1fe8-43c8-b3fe-b726d785c0e0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8a44de3-054d-44f4-8d22-b6c7286ce135");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b5fd232d-844d-4b4f-baca-3ca1bed47633", "AQAAAAIAAYagAAAAEFLD1xukoVGTvFmr34uC/eCQ9U8o6mQvYT8fAyFC6YXDvU7oP2H5up5mAoIVqXRLYg==", "c65e5d70-1442-41e7-b220-774d4b468901" });
        }
    }
}

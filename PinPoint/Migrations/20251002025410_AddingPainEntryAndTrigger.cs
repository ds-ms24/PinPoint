using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class AddingPainEntryAndTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PainEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EntryTime = table.Column<DateOnly>(type: "date", nullable: false),
                    PainIntensity = table.Column<int>(type: "integer", nullable: false),
                    PainDescription = table.Column<string>(type: "text", nullable: false),
                    ActivitiesBeforePain = table.Column<string>(type: "text", nullable: false),
                    ReliefMethodsTried = table.Column<string>(type: "text", nullable: false),
                    ReliefEffectiveness = table.Column<int>(type: "integer", nullable: false),
                    AdditionalNotes = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PainEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trigger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trigger", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "035c1331-d359-4594-aab2-bc79b6368cc6", "AQAAAAIAAYagAAAAEApAgf53QRpIBsOEsyT+GkgK7qxognrK0iVCG6oJNaXEbmWBQwqyKsyr+RycyXyOGQ==", "8febeead-2355-42df-a9a0-affc2e0918a9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PainEntries");

            migrationBuilder.DropTable(
                name: "Trigger");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b2b9b9a-aa86-4ccf-a74e-07819886e52d", "AQAAAAIAAYagAAAAEIQcK4k/MJdTFyUciLs3VCTHyOfBGDA2Saj71txaOqU/7xH3e+tH5E4i4pie2i/c8g==", "803dd53f-1fe8-43c8-b3fe-b726d785c0e0" });
        }
    }
}

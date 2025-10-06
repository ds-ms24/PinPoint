using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class AddPainEntrySymptomRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PainEntrySymptom",
                columns: table => new
                {
                    PainEntryId = table.Column<int>(type: "integer", nullable: false),
                    SymptomId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PainEntrySymptom", x => new { x.PainEntryId, x.SymptomId });
                    table.ForeignKey(
                        name: "FK_PainEntrySymptom_PainEntries_PainEntryId",
                        column: x => x.PainEntryId,
                        principalTable: "PainEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PainEntrySymptom_Symptoms_SymptomId",
                        column: x => x.SymptomId,
                        principalTable: "Symptoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "285700d3-681d-4a2e-bcaf-4829c170dbf1", "AQAAAAIAAYagAAAAEOsfSamJ2u9CG6/nzlsIdCt/BP4VERg3gClpMMwkk2mce9i7luM/IYoB0EEhPADJyg==", "07a42b3a-8867-4e45-8f3d-656decca3a8f" });

            migrationBuilder.CreateIndex(
                name: "IX_PainEntrySymptom_SymptomId",
                table: "PainEntrySymptom",
                column: "SymptomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PainEntrySymptom");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11444810-9058-4f7a-90ad-fcec9dd22501", "AQAAAAIAAYagAAAAEJlrUrBwiVukHMW4AR3AdcaAamdzyyjqL8XHCFWolyA+sYhR1CF4qacamFfyYnVV0Q==", "9c411f2e-4fca-469a-8e28-2bed5260ff73" });
        }
    }
}

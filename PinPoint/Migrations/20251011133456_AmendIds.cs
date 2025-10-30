using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class AmendIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PainEntrySymptom_Symptoms_SymptomId",
                table: "PainEntrySymptom");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e8ff371-5fc4-4335-a494-7dbecefed492", "AQAAAAIAAYagAAAAEKN856FmD62RdMHltIDraJZcQXTHgIZH3cST5fq0KQlODJ6YNuST2QACdPSZyUqcAQ==", "256882a3-717f-45b4-b898-dcc1274e2371" });

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_Name",
                table: "Triggers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Name",
                table: "Locations",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PainEntrySymptom_Symptoms_SymptomId",
                table: "PainEntrySymptom",
                column: "SymptomId",
                principalTable: "Symptoms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PainEntrySymptom_Symptoms_SymptomId",
                table: "PainEntrySymptom");

            migrationBuilder.DropIndex(
                name: "IX_Triggers_Name",
                table: "Triggers");

            migrationBuilder.DropIndex(
                name: "IX_Locations_Name",
                table: "Locations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1351aacf-35d7-4f08-8742-6b5bd6fe740a", "AQAAAAIAAYagAAAAELs9ETTPRQssgIcAUD0ULVEtHiYnNyG+wyhsApDE4NGCrUm9BbrEK7JOunU9Cvyu7A==", "b1ba0d75-55f3-4630-b6d4-eca119173efd" });

            migrationBuilder.AddForeignKey(
                name: "FK_PainEntrySymptom_Symptoms_SymptomId",
                table: "PainEntrySymptom",
                column: "SymptomId",
                principalTable: "Symptoms",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

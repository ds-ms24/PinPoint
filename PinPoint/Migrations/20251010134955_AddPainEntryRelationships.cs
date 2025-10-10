using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class AddPainEntryRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "PainEntries",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TriggerId",
                table: "PainEntries",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1351aacf-35d7-4f08-8742-6b5bd6fe740a", "AQAAAAIAAYagAAAAELs9ETTPRQssgIcAUD0ULVEtHiYnNyG+wyhsApDE4NGCrUm9BbrEK7JOunU9Cvyu7A==", "b1ba0d75-55f3-4630-b6d4-eca119173efd" });

            migrationBuilder.CreateIndex(
                name: "IX_PainEntries_LocationId",
                table: "PainEntries",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PainEntries_TriggerId",
                table: "PainEntries",
                column: "TriggerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PainEntries_Locations_LocationId",
                table: "PainEntries",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PainEntries_Triggers_TriggerId",
                table: "PainEntries",
                column: "TriggerId",
                principalTable: "Triggers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PainEntries_Locations_LocationId",
                table: "PainEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_PainEntries_Triggers_TriggerId",
                table: "PainEntries");

            migrationBuilder.DropIndex(
                name: "IX_PainEntries_LocationId",
                table: "PainEntries");

            migrationBuilder.DropIndex(
                name: "IX_PainEntries_TriggerId",
                table: "PainEntries");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "PainEntries");

            migrationBuilder.DropColumn(
                name: "TriggerId",
                table: "PainEntries");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58280577-9848-4f4f-b533-b36d55f157f9", "AQAAAAIAAYagAAAAEPYopqTK0wL7+vXpRNQ0yHsKxo3QqkMaslr+wjB55KnYH2RAC8zvpyrBAorRm7jBog==", "205fce75-16c0-4774-9bc1-dc26c0c1251e" });
        }
    }
}

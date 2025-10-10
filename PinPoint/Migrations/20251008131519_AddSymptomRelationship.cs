using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class AddSymptomRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PainEntrySymptom_Symptoms_SymptomId",
                table: "PainEntrySymptom");

            migrationBuilder.DropForeignKey(
                name: "FK_PainEntryTrigger_Trigger_TriggerId",
                table: "PainEntryTrigger");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trigger",
                table: "Trigger");

            migrationBuilder.RenameTable(
                name: "Trigger",
                newName: "Triggers");

            migrationBuilder.AddColumn<int>(
                name: "SymptomId",
                table: "PainEntries",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Triggers",
                table: "Triggers",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58280577-9848-4f4f-b533-b36d55f157f9", "AQAAAAIAAYagAAAAEPYopqTK0wL7+vXpRNQ0yHsKxo3QqkMaslr+wjB55KnYH2RAC8zvpyrBAorRm7jBog==", "205fce75-16c0-4774-9bc1-dc26c0c1251e" });

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_Name",
                table: "Symptoms",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PainEntries_SymptomId",
                table: "PainEntries",
                column: "SymptomId");

            migrationBuilder.AddForeignKey(
                name: "FK_PainEntries_Symptoms_SymptomId",
                table: "PainEntries",
                column: "SymptomId",
                principalTable: "Symptoms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PainEntrySymptom_Symptoms_SymptomId",
                table: "PainEntrySymptom",
                column: "SymptomId",
                principalTable: "Symptoms",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PainEntryTrigger_Triggers_TriggerId",
                table: "PainEntryTrigger",
                column: "TriggerId",
                principalTable: "Triggers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PainEntries_Symptoms_SymptomId",
                table: "PainEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_PainEntrySymptom_Symptoms_SymptomId",
                table: "PainEntrySymptom");

            migrationBuilder.DropForeignKey(
                name: "FK_PainEntryTrigger_Triggers_TriggerId",
                table: "PainEntryTrigger");

            migrationBuilder.DropIndex(
                name: "IX_Symptoms_Name",
                table: "Symptoms");

            migrationBuilder.DropIndex(
                name: "IX_PainEntries_SymptomId",
                table: "PainEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Triggers",
                table: "Triggers");

            migrationBuilder.DropColumn(
                name: "SymptomId",
                table: "PainEntries");

            migrationBuilder.RenameTable(
                name: "Triggers",
                newName: "Trigger");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trigger",
                table: "Trigger",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2691a172-afd0-43a2-8fc6-34a5d1f0d7c3", "AQAAAAIAAYagAAAAEPNcCKSwf9P7mYamfNfSoNC21zpcGdUM/IXASPoD97jfIZqfB0LxDBDKzV9lvVlRmQ==", "2598b8c0-1197-4442-a829-171055721d40" });

            migrationBuilder.AddForeignKey(
                name: "FK_PainEntrySymptom_Symptoms_SymptomId",
                table: "PainEntrySymptom",
                column: "SymptomId",
                principalTable: "Symptoms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PainEntryTrigger_Trigger_TriggerId",
                table: "PainEntryTrigger",
                column: "TriggerId",
                principalTable: "Trigger",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

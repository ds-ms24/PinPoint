using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class AddPainEntryTriggerRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PainEntryTrigger",
                columns: table => new
                {
                    PainEntryId = table.Column<int>(type: "integer", nullable: false),
                    TriggerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PainEntryTrigger", x => new { x.PainEntryId, x.TriggerId });
                    table.ForeignKey(
                        name: "FK_PainEntryTrigger_PainEntries_PainEntryId",
                        column: x => x.PainEntryId,
                        principalTable: "PainEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PainEntryTrigger_Trigger_TriggerId",
                        column: x => x.TriggerId,
                        principalTable: "Trigger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f48670e-48e5-4ef8-925e-5ad260b4b186", "AQAAAAIAAYagAAAAED0WKkWTgUsJ8EC0An5J+wiiY1ylJ8s3dSt6b5vjOK7a6BSdJNeimoFJKkiEj2ReUQ==", "f9e9863a-ac1b-488c-82da-dfa3ed617117" });

            migrationBuilder.CreateIndex(
                name: "IX_PainEntryTrigger_TriggerId",
                table: "PainEntryTrigger",
                column: "TriggerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PainEntryTrigger");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "035c1331-d359-4594-aab2-bc79b6368cc6", "AQAAAAIAAYagAAAAEApAgf53QRpIBsOEsyT+GkgK7qxognrK0iVCG6oJNaXEbmWBQwqyKsyr+RycyXyOGQ==", "8febeead-2355-42df-a9a0-affc2e0918a9" });
        }
    }
}

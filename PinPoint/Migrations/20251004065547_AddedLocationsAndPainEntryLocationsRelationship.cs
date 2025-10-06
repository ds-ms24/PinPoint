using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class AddedLocationsAndPainEntryLocationsRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PainEntryLocation",
                columns: table => new
                {
                    PainEntryId = table.Column<int>(type: "integer", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PainEntryLocation", x => new { x.PainEntryId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_PainEntryLocation_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PainEntryLocation_PainEntries_PainEntryId",
                        column: x => x.PainEntryId,
                        principalTable: "PainEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5db58518-724b-45a1-99a1-91933a9c0d26", "AQAAAAIAAYagAAAAEM/aGQWQDxbCgUFfR/qe7ENgN8bck6EI+2yOR5bUWjpAWkok7iYM0gOF90Tmfugdyw==", "7be4b47d-539a-4a12-a6bf-60933ed50bbc" });

            migrationBuilder.CreateIndex(
                name: "IX_PainEntryLocation_LocationId",
                table: "PainEntryLocation",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PainEntryLocation");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "285700d3-681d-4a2e-bcaf-4829c170dbf1", "AQAAAAIAAYagAAAAEOsfSamJ2u9CG6/nzlsIdCt/BP4VERg3gClpMMwkk2mce9i7luM/IYoB0EEhPADJyg==", "07a42b3a-8867-4e45-8f3d-656decca3a8f" });
        }
    }
}

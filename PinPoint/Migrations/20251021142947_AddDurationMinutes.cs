using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class AddDurationMinutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationMinutes",
                table: "PainEntries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ca02ddf-db68-400e-ace9-7bffed6f8095", "AQAAAAIAAYagAAAAEBuLBGSlLYuP+GgL4AECF1saam45O++cpzpT05Pm/K00vR7JmYJD+z3nMs5dYrm/tQ==", "6168fd82-7746-4cbe-a9bc-eb7720364dcb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationMinutes",
                table: "PainEntries");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e8ff371-5fc4-4335-a494-7dbecefed492", "AQAAAAIAAYagAAAAEKN856FmD62RdMHltIDraJZcQXTHgIZH3cST5fq0KQlODJ6YNuST2QACdPSZyUqcAQ==", "256882a3-717f-45b4-b898-dcc1274e2371" });
        }
    }
}

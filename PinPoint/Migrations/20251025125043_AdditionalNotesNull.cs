using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalNotesNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c1ac7ab-11d5-49b5-a4eb-c11706478bc4", "AQAAAAIAAYagAAAAEEQhMdTf17FXe9OfnXMeM+SgYr78OFnzH0VerNh6FhCtS33AAzM5xHiMa0yU79HeLw==", "c6fb5db0-dca3-4726-bf05-8d2a1eb0b980" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ca02ddf-db68-400e-ace9-7bffed6f8095", "AQAAAAIAAYagAAAAEBuLBGSlLYuP+GgL4AECF1saam45O++cpzpT05Pm/K00vR7JmYJD+z3nMs5dYrm/tQ==", "6168fd82-7746-4cbe-a9bc-eb7720364dcb" });
        }
    }
}

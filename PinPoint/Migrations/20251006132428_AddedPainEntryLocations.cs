using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class AddedPainEntryLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PainEntryLocation_Location_LocationId",
                table: "PainEntryLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            //migrationBuilder.AlterColumn<TimeOnly>(
            //    name: "EntryTime",
            //    table: "PainEntries",
            //    type: "time without time zone",
            //    nullable: false,
            //    oldClrType: typeof(DateOnly),
            //    oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c43227f-8732-4085-b000-f59ab30cddff", "AQAAAAIAAYagAAAAEJKCNnOVN1s1WMMM/isa30ZpXWRm0crXiA7zmV2G5FF+CXnF1idogad4nNIutCmHBQ==", "aba76064-4404-4f6b-ab08-c00fe6db89f6" });

            migrationBuilder.AddForeignKey(
                name: "FK_PainEntryLocation_Locations_LocationId",
                table: "PainEntryLocation",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PainEntryLocation_Locations_LocationId",
                table: "PainEntryLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            //migrationBuilder.AlterColumn<DateOnly>(
            //    name: "EntryTime",
            //    table: "PainEntries",
            //    type: "date",
            //    nullable: false,
            //    oldClrType: typeof(TimeOnly),
            //    oldType: "time without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5db58518-724b-45a1-99a1-91933a9c0d26", "AQAAAAIAAYagAAAAEM/aGQWQDxbCgUFfR/qe7ENgN8bck6EI+2yOR5bUWjpAWkok7iYM0gOF90Tmfugdyw==", "7be4b47d-539a-4a12-a6bf-60933ed50bbc" });

            migrationBuilder.AddForeignKey(
                name: "FK_PainEntryLocation_Location_LocationId",
                table: "PainEntryLocation",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

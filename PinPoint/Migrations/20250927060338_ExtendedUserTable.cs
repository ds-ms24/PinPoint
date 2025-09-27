using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b5fd232d-844d-4b4f-baca-3ca1bed47633", new DateOnly(1990, 9, 5), "Default", "Admin", "AQAAAAIAAYagAAAAEFLD1xukoVGTvFmr34uC/eCQ9U8o6mQvYT8fAyFC6YXDvU7oP2H5up5mAoIVqXRLYg==", "c65e5d70-1442-41e7-b220-774d4b468901" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d4260287-f780-4ce4-b1e9-08148f08d929", "AQAAAAIAAYagAAAAEC+BGihFcgEmeP1mC1sgkDogiipFOWiEwdbF3Bz7GIfuigT/2AFCrLPueQ/+Pe0Zeg==", "1d3b76d8-10e1-442f-8e90-2395828f343f" });
        }
    }
}

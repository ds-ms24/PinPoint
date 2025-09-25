using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class init_user_and_roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3f720a9f-e689-41fb-b3a9-52a07410bf5f", null, "Developer", "DEVELOPER" },
                    { "4bf6db49-c852-409f-900f-48a83f70047b", null, "Employee", "EMPLOYEE" },
                    { "7ff01497-5a99-4470-87ee-d8e8a267a99b", null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4", 0, "d4260287-f780-4ce4-b1e9-08148f08d929", "dev@pinpoint.com.au", true, false, null, "DEV@PINPOINT.COM.AU", "DEV@PINPOINT.COM.AU", "AQAAAAIAAYagAAAAEC+BGihFcgEmeP1mC1sgkDogiipFOWiEwdbF3Bz7GIfuigT/2AFCrLPueQ/+Pe0Zeg==", null, false, "1d3b76d8-10e1-442f-8e90-2395828f343f", false, "dev@pinpoint.com.au" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3f720a9f-e689-41fb-b3a9-52a07410bf5f", "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4bf6db49-c852-409f-900f-48a83f70047b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ff01497-5a99-4470-87ee-d8e8a267a99b");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3f720a9f-e689-41fb-b3a9-52a07410bf5f", "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f720a9f-e689-41fb-b3a9-52a07410bf5f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4");
        }
    }
}

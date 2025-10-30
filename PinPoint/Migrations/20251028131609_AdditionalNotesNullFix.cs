using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalNotesNullFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdditionalNotes",
                table: "PainEntries",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72faf963-c2f5-41b4-96c0-3fd216be915e", "AQAAAAIAAYagAAAAEFNP/3YFDqMYg4w+DwEI9ZpYyVofKp+u5aoKUjMV6fULTiNGeTt+aEBsUEyrUOGVng==", "7fc29144-8db4-42b9-8394-3fff20303a6b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdditionalNotes",
                table: "PainEntries",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c1ac7ab-11d5-49b5-a4eb-c11706478bc4", "AQAAAAIAAYagAAAAEEQhMdTf17FXe9OfnXMeM+SgYr78OFnzH0VerNh6FhCtS33AAzM5xHiMa0yU79HeLw==", "c6fb5db0-dca3-4726-bf05-8d2a1eb0b980" });
        }
    }
}

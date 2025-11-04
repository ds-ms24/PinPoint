using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeleteRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PainEntryId = table.Column<int>(type: "integer", nullable: false),
                    RequestedByUserId = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    Reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    RequestedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RequestedTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ReviewedByUserId = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    ReviewedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReviewerNotes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeleteRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeleteRequests_PainEntries_PainEntryId",
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
                values: new object[] { "c69cf785-f1cd-461d-9af3-d66ca6d0b9a3", "AQAAAAIAAYagAAAAEFpYVqQR5CyfSIPUcLcVEXiEI+Ls3x7CzIOVDrXj72YhaxjPyjFTtxb5Lmo7FQgglA==", "47db3051-4a73-48a6-9a29-e9bcc97e5bf0" });

            migrationBuilder.CreateIndex(
                name: "IX_DeleteRequests_PainEntryId",
                table: "DeleteRequests",
                column: "PainEntryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeleteRequests");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72faf963-c2f5-41b4-96c0-3fd216be915e", "AQAAAAIAAYagAAAAEFNP/3YFDqMYg4w+DwEI9ZpYyVofKp+u5aoKUjMV6fULTiNGeTt+aEBsUEyrUOGVng==", "7fc29144-8db4-42b9-8394-3fff20303a6b" });
        }
    }
}

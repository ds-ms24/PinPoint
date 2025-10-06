using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinPoint.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEntryTimeToTimeOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Add a temporary column of the new type
            migrationBuilder.AddColumn<TimeOnly>(
                name: "EntryTimeTemp",
                table: "PainEntries",
                type: "time without time zone",
                nullable: true);

            // 2. Copy the time part from the old column (PostgreSQL syntax)
            migrationBuilder.Sql(
                @"UPDATE ""PainEntries"" SET ""EntryTimeTemp"" = TO_CHAR(""EntryTime"", 'HH24:MI:SS')::time");

            // 3. Drop the old column
            migrationBuilder.DropColumn(
                name: "EntryTime",
                table: "PainEntries");

            // 4. Rename the new column
            migrationBuilder.RenameColumn(
                name: "EntryTimeTemp",
                table: "PainEntries",
                newName: "EntryTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 1. Add the old column back
            migrationBuilder.AddColumn<DateOnly>(
                name: "EntryTimeTemp",
                table: "PainEntries",
                type: "date",
                nullable: true);

            // 2. Copy the date part from the new column (if needed)
            migrationBuilder.Sql(
                @"UPDATE ""PainEntries"" SET ""EntryTimeTemp"" = CURRENT_DATE"); // or use a default

            // 3. Drop the new column
            migrationBuilder.DropColumn(
                name: "EntryTime",
                table: "PainEntries");

            // 4. Rename back
            migrationBuilder.RenameColumn(
                name: "EntryTimeTemp",
                table: "PainEntries",
                newName: "EntryTime");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeedingDataDiaryEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DiaryEntries",
                keyColumn: "DiaryEntryId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "DiaryEntries",
                keyColumn: "DiaryEntryId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "DiaryEntries",
                keyColumn: "DiaryEntryId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "DiaryEntries",
                keyColumn: "DiaryEntryId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Local));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DiaryEntries",
                keyColumn: "DiaryEntryId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2026, 6, 11, 12, 20, 54, 197, DateTimeKind.Local).AddTicks(3014));

            migrationBuilder.UpdateData(
                table: "DiaryEntries",
                keyColumn: "DiaryEntryId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2026, 6, 11, 12, 20, 54, 198, DateTimeKind.Local).AddTicks(6584));

            migrationBuilder.UpdateData(
                table: "DiaryEntries",
                keyColumn: "DiaryEntryId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2026, 6, 11, 12, 20, 54, 198, DateTimeKind.Local).AddTicks(6597));

            migrationBuilder.UpdateData(
                table: "DiaryEntries",
                keyColumn: "DiaryEntryId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2026, 6, 11, 12, 20, 54, 198, DateTimeKind.Local).AddTicks(6599));
        }
    }
}

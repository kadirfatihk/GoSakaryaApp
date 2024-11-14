using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoSakaryaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class _20241105_v5_ticketupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 5, 20, 29, 15, 317, DateTimeKind.Utc).AddTicks(4859));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 5, 19, 18, 13, 687, DateTimeKind.Utc).AddTicks(5333));
        }
    }
}

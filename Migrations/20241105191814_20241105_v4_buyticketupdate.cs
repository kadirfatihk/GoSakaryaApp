using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoSakaryaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class _20241105_v4_buyticketupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketCapacity",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 5, 19, 18, 13, 687, DateTimeKind.Utc).AddTicks(5333));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketCapacity",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 4, 21, 35, 25, 981, DateTimeKind.Utc).AddTicks(3949));
        }
    }
}

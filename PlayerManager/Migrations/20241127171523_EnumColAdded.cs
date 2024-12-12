using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerManager.Migrations
{
    /// <inheritdoc />
    public partial class EnumColAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Position",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractExp",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Retired",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractExp",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Retired",
                table: "Players");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Players",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}

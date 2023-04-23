using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations.Ms
{
    /// <inheritdoc />
    public partial class InitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreateDate", "ModifiedDate", "Name", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 23, 21, 11, 24, 921, DateTimeKind.Local).AddTicks(5173), new DateTime(2023, 4, 23, 21, 11, 24, 921, DateTimeKind.Local).AddTicks(5187), "Başkandan Yazılar", true },
                    { 2, new DateTime(2023, 4, 23, 21, 11, 24, 921, DateTimeKind.Local).AddTicks(5189), new DateTime(2023, 4, 23, 21, 11, 24, 921, DateTimeKind.Local).AddTicks(5189), "Genel Sekreterden Yazılar", true },
                    { 3, new DateTime(2023, 4, 23, 21, 11, 24, 921, DateTimeKind.Local).AddTicks(5190), new DateTime(2023, 4, 23, 21, 11, 24, 921, DateTimeKind.Local).AddTicks(5191), "TV Programı ve Röportajlar", true },
                    { 4, new DateTime(2023, 4, 23, 21, 11, 24, 921, DateTimeKind.Local).AddTicks(5192), new DateTime(2023, 4, 23, 21, 11, 24, 921, DateTimeKind.Local).AddTicks(5193), "Etkinlikler", true },
                    { 5, new DateTime(2023, 4, 23, 21, 11, 24, 921, DateTimeKind.Local).AddTicks(5197), new DateTime(2023, 4, 23, 21, 11, 24, 921, DateTimeKind.Local).AddTicks(5197), "Genç İnder", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}

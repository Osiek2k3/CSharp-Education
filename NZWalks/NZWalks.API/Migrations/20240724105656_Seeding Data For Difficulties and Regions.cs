using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataForDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficuties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a5fcf75b-64c9-40c5-8d40-f8efe046cac9"), "Medium" },
                    { new Guid("e37a8d11-2538-4176-84c6-8def70893507"), "Easy" },
                    { new Guid("ecfd3dee-79b4-4d49-b60f-1ad2f245a159"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("298011b4-b9fe-4fc7-9722-4033f4f9a320"), "krk", "krakow", "krakow" },
                    { new Guid("3715645b-f994-4174-ad6f-9ba189b412c7"), "po", "poznan", "poznan" },
                    { new Guid("77f38b06-d675-4273-893b-98e4faf018b9"), "tkn", "konskie", "konskie.jpg" },
                    { new Guid("82bfdda6-aaeb-4367-b009-90c8ef7332ff"), "so", "sosnowiec", "sosonowiec" },
                    { new Guid("929a8a0e-580d-4aa7-abf2-d888b683e0c1"), "tki", "kielce", "kielce.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficuties",
                keyColumn: "Id",
                keyValue: new Guid("a5fcf75b-64c9-40c5-8d40-f8efe046cac9"));

            migrationBuilder.DeleteData(
                table: "Difficuties",
                keyColumn: "Id",
                keyValue: new Guid("e37a8d11-2538-4176-84c6-8def70893507"));

            migrationBuilder.DeleteData(
                table: "Difficuties",
                keyColumn: "Id",
                keyValue: new Guid("ecfd3dee-79b4-4d49-b60f-1ad2f245a159"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("298011b4-b9fe-4fc7-9722-4033f4f9a320"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3715645b-f994-4174-ad6f-9ba189b412c7"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("77f38b06-d675-4273-893b-98e4faf018b9"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("82bfdda6-aaeb-4367-b009-90c8ef7332ff"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("929a8a0e-580d-4aa7-abf2-d888b683e0c1"));
        }
    }
}

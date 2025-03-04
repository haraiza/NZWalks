using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("52ae6919-ed9c-4f59-b684-e6dd966411fc"), "Medium" },
                    { new Guid("574f113d-d1b2-48aa-a7b0-c4b083e0cc9f"), "Hard" },
                    { new Guid("f6f671a5-c591-4737-97b4-e4c18b17019d"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Region",
                columns: new[] { "Id", "Code", "Name", "RegionImageURL" },
                values: new object[,]
                {
                    { new Guid("5cdf0c65-5b71-481f-a1ee-d6d2c3c7a842"), "AKL", "Auckland", null },
                    { new Guid("5de6ad77-2e24-4902-b921-12f7daed208a"), "BOP", "Bay Of Plenty", null },
                    { new Guid("7a18137d-388f-4da7-a122-ba610f9d29fc"), "NTL", "Northland", null },
                    { new Guid("813765d6-56e9-495a-b3df-636f81f11241"), "NSN", "Nelson", null },
                    { new Guid("83325d54-42d5-48d2-8867-63d457718080"), "WGN", "Wellington", null },
                    { new Guid("8bc47a6a-a25b-4ad1-a7fb-9f7caa02a321"), "STL", "Southland", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("52ae6919-ed9c-4f59-b684-e6dd966411fc"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("574f113d-d1b2-48aa-a7b0-c4b083e0cc9f"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f6f671a5-c591-4737-97b4-e4c18b17019d"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("5cdf0c65-5b71-481f-a1ee-d6d2c3c7a842"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("5de6ad77-2e24-4902-b921-12f7daed208a"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("7a18137d-388f-4da7-a122-ba610f9d29fc"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("813765d6-56e9-495a-b3df-636f81f11241"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("83325d54-42d5-48d2-8867-63d457718080"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("8bc47a6a-a25b-4ad1-a7fb-9f7caa02a321"));
        }
    }
}

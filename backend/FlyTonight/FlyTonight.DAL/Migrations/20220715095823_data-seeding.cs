using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyTonight.DAL.Migrations
{
    public partial class dataseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountFlight_Discounts_DiscountsId",
                table: "DiscountFlight");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountFlight_Flights_FlightsId",
                table: "DiscountFlight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscountFlight",
                table: "DiscountFlight");

            migrationBuilder.RenameTable(
                name: "DiscountFlight",
                newName: "FlightDiscount");

            migrationBuilder.RenameIndex(
                name: "IX_DiscountFlight_FlightsId",
                table: "FlightDiscount",
                newName: "IX_FlightDiscount_FlightsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightDiscount",
                table: "FlightDiscount",
                columns: new[] { "DiscountsId", "FlightsId" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "Id", "BlobUrl", "CityName", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { new Guid("99961c57-3a5b-440a-86e1-a5aef4ae33e1"), "http://127.0.0.1:10000/devstoreaccount1/airports/bud_logo.png", "Budapest", 47.431342998981528, 19.266202715990577, "BUD" },
                    { new Guid("a6c28835-277e-44f1-878e-d7369c4993dd"), "http://127.0.0.1:10000/devstoreaccount1/airports/lut_logo.png", "London", 51.879341283924852, -0.37626272040543279, "LUT" },
                    { new Guid("f2c82559-be2d-4aa3-ad88-3db28eaf3d34"), "http://127.0.0.1:10000/devstoreaccount1/airports/ams_logo.jpg", "Amsterdam", 52.30710006669635, 4.7677723543914201, "AMS" }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "BlobUrl", "Name", "Value" },
                values: new object[] { new Guid("7abeec0a-8840-4e72-af65-94989aa32c9d"), "emptyurl", "Weekend", 0.10000000000000001 });

            migrationBuilder.InsertData(
                table: "Partners",
                columns: new[] { "Id", "BlobUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("5d00437d-48da-4b8d-9c74-65f357f182ee"), "http://127.0.0.1:10000/devstoreaccount1/partners/executiveclub.jpg", "Executive Club" },
                    { new Guid("637431e4-c72f-4fc6-b5c7-97fe75bc7441"), "http://127.0.0.1:10000/devstoreaccount1/partners/staralliance.png", "Star Alliance" },
                    { new Guid("b79ea8b8-ca6b-47a3-8848-ba1d32f3eeee"), "http://127.0.0.1:10000/devstoreaccount1/partners/asiamiles.jpg", "Asia Miles" },
                    { new Guid("c5c95b8a-bbca-4bcd-a5e9-e5c4517c33e1"), "http://127.0.0.1:10000/devstoreaccount1/partners/mileageplan.jpg", "Mileage Plan" },
                    { new Guid("e19cf9a7-f629-40ef-b0f3-56474e1c9a91"), "http://127.0.0.1:10000/devstoreaccount1/partners/aeroplan.jpg", "Aeroplan" },
                    { new Guid("f4113039-9133-4195-ba3b-17656e4dfecf"), "http://127.0.0.1:10000/devstoreaccount1/partners/oneworld.png", "Oneworld" }
                });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "Id", "CruiseHeight", "CruiseSpeed", "FlightDistance", "FuselageLength", "Registration", "SeatColCount", "SeatRowCount", "Type", "Wingspan" },
                values: new object[,]
                {
                    { new Guid("08030bd5-b032-42d1-87ab-42141e1e484c"), 12500, 0.78500000000000003, 5575, 40.0, "HA-QWE", 6, 29, "B738", 34.0 },
                    { new Guid("67ec6d8e-eb94-4c6e-9a7a-6a0995895d70"), 12000, 0.81999999999999995, 6390, 39.0, "HA-ABC", 4, 27, "A221", 35.0 },
                    { new Guid("6aaa9938-7900-4316-9c5c-9621c410cd52"), 12000, 0.81999999999999995, 6390, 39.0, "HA-DEF", 4, 27, "A221", 35.0 },
                    { new Guid("8c73349b-2c71-47fd-9bbe-1cbb81f71f48"), 12100, 0.78000000000000003, 5930, 46.0, "HA-VEL", 6, 31, "A321", 36.0 },
                    { new Guid("c17a3c19-7420-4f75-bc15-cad8cc100803"), 12100, 0.78000000000000003, 5930, 46.0, "HA-RTZ", 6, 31, "A321", 36.0 },
                    { new Guid("d88685cb-278e-4cee-814e-241955c1cd24"), 12500, 0.78500000000000003, 5575, 40.0, "HA-KJL", 6, 29, "B738", 34.0 },
                    { new Guid("fe76ac24-c8b6-4d68-9f2a-4b86893e0ad5"), 12500, 0.78500000000000003, 5575, 40.0, "HA-CCV", 6, 29, "B738", 34.0 }
                });

            migrationBuilder.InsertData(
                table: "Taxes",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[,]
                {
                    { new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1"), "DepartureTax", 5000 },
                    { new Guid("0b6bd721-a40d-43e2-affc-de1646f85f8b"), "FlatTax", 2000 }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "AirplaneId", "DebuffId", "FromId", "TimeOfDeparture", "ToId" },
                values: new object[,]
                {
                    { new Guid("0237ecf2-85e3-4873-bef6-78ae56322cc4"), new Guid("8c73349b-2c71-47fd-9bbe-1cbb81f71f48"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("99961c57-3a5b-440a-86e1-a5aef4ae33e1"), new DateTime(2022, 8, 12, 10, 30, 0, 0, DateTimeKind.Unspecified), new Guid("f2c82559-be2d-4aa3-ad88-3db28eaf3d34") },
                    { new Guid("15a6343e-1780-4a41-b730-dd7e4c75eb55"), new Guid("67ec6d8e-eb94-4c6e-9a7a-6a0995895d70"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("99961c57-3a5b-440a-86e1-a5aef4ae33e1"), new DateTime(2022, 8, 12, 6, 30, 0, 0, DateTimeKind.Unspecified), new Guid("f2c82559-be2d-4aa3-ad88-3db28eaf3d34") },
                    { new Guid("4e8949ec-404c-4e5f-a47e-0898f8800a1e"), new Guid("fe76ac24-c8b6-4d68-9f2a-4b86893e0ad5"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("99961c57-3a5b-440a-86e1-a5aef4ae33e1"), new DateTime(2022, 8, 12, 18, 30, 0, 0, DateTimeKind.Unspecified), new Guid("a6c28835-277e-44f1-878e-d7369c4993dd") },
                    { new Guid("5cae9352-4511-40eb-a7f4-de0fdb9c2cbb"), new Guid("67ec6d8e-eb94-4c6e-9a7a-6a0995895d70"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("99961c57-3a5b-440a-86e1-a5aef4ae33e1"), new DateTime(2022, 8, 13, 6, 30, 0, 0, DateTimeKind.Unspecified), new Guid("f2c82559-be2d-4aa3-ad88-3db28eaf3d34") },
                    { new Guid("6f01eab3-9be3-497d-b7ec-6a311bdc2b34"), new Guid("d88685cb-278e-4cee-814e-241955c1cd24"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("f2c82559-be2d-4aa3-ad88-3db28eaf3d34"), new DateTime(2022, 8, 14, 13, 30, 0, 0, DateTimeKind.Unspecified), new Guid("99961c57-3a5b-440a-86e1-a5aef4ae33e1") },
                    { new Guid("70a1b51c-d009-44d0-ad08-c43cf5d0ab15"), new Guid("67ec6d8e-eb94-4c6e-9a7a-6a0995895d70"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("99961c57-3a5b-440a-86e1-a5aef4ae33e1"), new DateTime(2022, 8, 12, 20, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f2c82559-be2d-4aa3-ad88-3db28eaf3d34") },
                    { new Guid("79ccbdb8-a022-4e3b-915f-974928cdf363"), new Guid("08030bd5-b032-42d1-87ab-42141e1e484c"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("99961c57-3a5b-440a-86e1-a5aef4ae33e1"), new DateTime(2022, 8, 12, 14, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f2c82559-be2d-4aa3-ad88-3db28eaf3d34") },
                    { new Guid("7bc038f9-80bf-46dc-8c29-8c94b732baf8"), new Guid("d88685cb-278e-4cee-814e-241955c1cd24"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("99961c57-3a5b-440a-86e1-a5aef4ae33e1"), new DateTime(2022, 8, 13, 18, 30, 0, 0, DateTimeKind.Unspecified), new Guid("f2c82559-be2d-4aa3-ad88-3db28eaf3d34") },
                    { new Guid("93d4df2d-038c-4a6a-bc76-035b019fe62e"), new Guid("67ec6d8e-eb94-4c6e-9a7a-6a0995895d70"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("f2c82559-be2d-4aa3-ad88-3db28eaf3d34"), new DateTime(2022, 8, 14, 19, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99961c57-3a5b-440a-86e1-a5aef4ae33e1") },
                    { new Guid("9acb9c67-db21-4eb5-85b1-b7c189171b14"), new Guid("c17a3c19-7420-4f75-bc15-cad8cc100803"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("f2c82559-be2d-4aa3-ad88-3db28eaf3d34"), new DateTime(2022, 8, 15, 12, 15, 0, 0, DateTimeKind.Unspecified), new Guid("99961c57-3a5b-440a-86e1-a5aef4ae33e1") },
                    { new Guid("a62e1436-9533-4635-b3ec-6a072db2031a"), new Guid("d88685cb-278e-4cee-814e-241955c1cd24"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("99961c57-3a5b-440a-86e1-a5aef4ae33e1"), new DateTime(2022, 8, 12, 16, 30, 0, 0, DateTimeKind.Unspecified), new Guid("f2c82559-be2d-4aa3-ad88-3db28eaf3d34") },
                    { new Guid("bcfbd6fa-89be-45f9-956d-8ad2f40c3cfa"), new Guid("6aaa9938-7900-4316-9c5c-9621c410cd52"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("99961c57-3a5b-440a-86e1-a5aef4ae33e1"), new DateTime(2022, 8, 13, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f2c82559-be2d-4aa3-ad88-3db28eaf3d34") },
                    { new Guid("be251ce7-fb8b-49e7-b178-97297ca4c59c"), new Guid("6aaa9938-7900-4316-9c5c-9621c410cd52"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("99961c57-3a5b-440a-86e1-a5aef4ae33e1"), new DateTime(2022, 8, 12, 8, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f2c82559-be2d-4aa3-ad88-3db28eaf3d34") },
                    { new Guid("ff5c38d8-5662-4d97-b093-9b8e0f53c057"), new Guid("08030bd5-b032-42d1-87ab-42141e1e484c"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("f2c82559-be2d-4aa3-ad88-3db28eaf3d34"), new DateTime(2022, 8, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99961c57-3a5b-440a-86e1-a5aef4ae33e1") }
                });

            migrationBuilder.InsertData(
                table: "FlightDiscount",
                columns: new[] { "DiscountsId", "FlightsId" },
                values: new object[,]
                {
                    { new Guid("7abeec0a-8840-4e72-af65-94989aa32c9d"), new Guid("5cae9352-4511-40eb-a7f4-de0fdb9c2cbb") },
                    { new Guid("7abeec0a-8840-4e72-af65-94989aa32c9d"), new Guid("6f01eab3-9be3-497d-b7ec-6a311bdc2b34") },
                    { new Guid("7abeec0a-8840-4e72-af65-94989aa32c9d"), new Guid("7bc038f9-80bf-46dc-8c29-8c94b732baf8") },
                    { new Guid("7abeec0a-8840-4e72-af65-94989aa32c9d"), new Guid("93d4df2d-038c-4a6a-bc76-035b019fe62e") },
                    { new Guid("7abeec0a-8840-4e72-af65-94989aa32c9d"), new Guid("bcfbd6fa-89be-45f9-956d-8ad2f40c3cfa") },
                    { new Guid("7abeec0a-8840-4e72-af65-94989aa32c9d"), new Guid("ff5c38d8-5662-4d97-b093-9b8e0f53c057") }
                });

            migrationBuilder.InsertData(
                table: "FlightTax",
                columns: new[] { "FlightsId", "TaxesId" },
                values: new object[,]
                {
                    { new Guid("0237ecf2-85e3-4873-bef6-78ae56322cc4"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") },
                    { new Guid("15a6343e-1780-4a41-b730-dd7e4c75eb55"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") },
                    { new Guid("4e8949ec-404c-4e5f-a47e-0898f8800a1e"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") },
                    { new Guid("5cae9352-4511-40eb-a7f4-de0fdb9c2cbb"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") },
                    { new Guid("6f01eab3-9be3-497d-b7ec-6a311bdc2b34"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") },
                    { new Guid("70a1b51c-d009-44d0-ad08-c43cf5d0ab15"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") },
                    { new Guid("79ccbdb8-a022-4e3b-915f-974928cdf363"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") },
                    { new Guid("7bc038f9-80bf-46dc-8c29-8c94b732baf8"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") },
                    { new Guid("93d4df2d-038c-4a6a-bc76-035b019fe62e"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") },
                    { new Guid("9acb9c67-db21-4eb5-85b1-b7c189171b14"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") },
                    { new Guid("a62e1436-9533-4635-b3ec-6a072db2031a"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") },
                    { new Guid("bcfbd6fa-89be-45f9-956d-8ad2f40c3cfa"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") },
                    { new Guid("be251ce7-fb8b-49e7-b178-97297ca4c59c"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") },
                    { new Guid("ff5c38d8-5662-4d97-b093-9b8e0f53c057"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FlightDiscount_Discounts_DiscountsId",
                table: "FlightDiscount",
                column: "DiscountsId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightDiscount_Flights_FlightsId",
                table: "FlightDiscount",
                column: "FlightsId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightDiscount_Discounts_DiscountsId",
                table: "FlightDiscount");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightDiscount_Flights_FlightsId",
                table: "FlightDiscount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightDiscount",
                table: "FlightDiscount");

            migrationBuilder.DeleteData(
                table: "FlightDiscount",
                keyColumns: new[] { "DiscountsId", "FlightsId" },
                keyValues: new object[] { new Guid("7abeec0a-8840-4e72-af65-94989aa32c9d"), new Guid("5cae9352-4511-40eb-a7f4-de0fdb9c2cbb") });

            migrationBuilder.DeleteData(
                table: "FlightDiscount",
                keyColumns: new[] { "DiscountsId", "FlightsId" },
                keyValues: new object[] { new Guid("7abeec0a-8840-4e72-af65-94989aa32c9d"), new Guid("6f01eab3-9be3-497d-b7ec-6a311bdc2b34") });

            migrationBuilder.DeleteData(
                table: "FlightDiscount",
                keyColumns: new[] { "DiscountsId", "FlightsId" },
                keyValues: new object[] { new Guid("7abeec0a-8840-4e72-af65-94989aa32c9d"), new Guid("7bc038f9-80bf-46dc-8c29-8c94b732baf8") });

            migrationBuilder.DeleteData(
                table: "FlightDiscount",
                keyColumns: new[] { "DiscountsId", "FlightsId" },
                keyValues: new object[] { new Guid("7abeec0a-8840-4e72-af65-94989aa32c9d"), new Guid("93d4df2d-038c-4a6a-bc76-035b019fe62e") });

            migrationBuilder.DeleteData(
                table: "FlightDiscount",
                keyColumns: new[] { "DiscountsId", "FlightsId" },
                keyValues: new object[] { new Guid("7abeec0a-8840-4e72-af65-94989aa32c9d"), new Guid("bcfbd6fa-89be-45f9-956d-8ad2f40c3cfa") });

            migrationBuilder.DeleteData(
                table: "FlightDiscount",
                keyColumns: new[] { "DiscountsId", "FlightsId" },
                keyValues: new object[] { new Guid("7abeec0a-8840-4e72-af65-94989aa32c9d"), new Guid("ff5c38d8-5662-4d97-b093-9b8e0f53c057") });

            migrationBuilder.DeleteData(
                table: "FlightTax",
                keyColumns: new[] { "FlightsId", "TaxesId" },
                keyValues: new object[] { new Guid("0237ecf2-85e3-4873-bef6-78ae56322cc4"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") });

            migrationBuilder.DeleteData(
                table: "FlightTax",
                keyColumns: new[] { "FlightsId", "TaxesId" },
                keyValues: new object[] { new Guid("15a6343e-1780-4a41-b730-dd7e4c75eb55"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") });

            migrationBuilder.DeleteData(
                table: "FlightTax",
                keyColumns: new[] { "FlightsId", "TaxesId" },
                keyValues: new object[] { new Guid("4e8949ec-404c-4e5f-a47e-0898f8800a1e"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") });

            migrationBuilder.DeleteData(
                table: "FlightTax",
                keyColumns: new[] { "FlightsId", "TaxesId" },
                keyValues: new object[] { new Guid("5cae9352-4511-40eb-a7f4-de0fdb9c2cbb"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") });

            migrationBuilder.DeleteData(
                table: "FlightTax",
                keyColumns: new[] { "FlightsId", "TaxesId" },
                keyValues: new object[] { new Guid("6f01eab3-9be3-497d-b7ec-6a311bdc2b34"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") });

            migrationBuilder.DeleteData(
                table: "FlightTax",
                keyColumns: new[] { "FlightsId", "TaxesId" },
                keyValues: new object[] { new Guid("70a1b51c-d009-44d0-ad08-c43cf5d0ab15"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") });

            migrationBuilder.DeleteData(
                table: "FlightTax",
                keyColumns: new[] { "FlightsId", "TaxesId" },
                keyValues: new object[] { new Guid("79ccbdb8-a022-4e3b-915f-974928cdf363"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") });

            migrationBuilder.DeleteData(
                table: "FlightTax",
                keyColumns: new[] { "FlightsId", "TaxesId" },
                keyValues: new object[] { new Guid("7bc038f9-80bf-46dc-8c29-8c94b732baf8"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") });

            migrationBuilder.DeleteData(
                table: "FlightTax",
                keyColumns: new[] { "FlightsId", "TaxesId" },
                keyValues: new object[] { new Guid("93d4df2d-038c-4a6a-bc76-035b019fe62e"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") });

            migrationBuilder.DeleteData(
                table: "FlightTax",
                keyColumns: new[] { "FlightsId", "TaxesId" },
                keyValues: new object[] { new Guid("9acb9c67-db21-4eb5-85b1-b7c189171b14"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") });

            migrationBuilder.DeleteData(
                table: "FlightTax",
                keyColumns: new[] { "FlightsId", "TaxesId" },
                keyValues: new object[] { new Guid("a62e1436-9533-4635-b3ec-6a072db2031a"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") });

            migrationBuilder.DeleteData(
                table: "FlightTax",
                keyColumns: new[] { "FlightsId", "TaxesId" },
                keyValues: new object[] { new Guid("bcfbd6fa-89be-45f9-956d-8ad2f40c3cfa"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") });

            migrationBuilder.DeleteData(
                table: "FlightTax",
                keyColumns: new[] { "FlightsId", "TaxesId" },
                keyValues: new object[] { new Guid("be251ce7-fb8b-49e7-b178-97297ca4c59c"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") });

            migrationBuilder.DeleteData(
                table: "FlightTax",
                keyColumns: new[] { "FlightsId", "TaxesId" },
                keyValues: new object[] { new Guid("ff5c38d8-5662-4d97-b093-9b8e0f53c057"), new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1") });

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("5d00437d-48da-4b8d-9c74-65f357f182ee"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("637431e4-c72f-4fc6-b5c7-97fe75bc7441"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("b79ea8b8-ca6b-47a3-8848-ba1d32f3eeee"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("c5c95b8a-bbca-4bcd-a5e9-e5c4517c33e1"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e19cf9a7-f629-40ef-b0f3-56474e1c9a91"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("f4113039-9133-4195-ba3b-17656e4dfecf"));

            migrationBuilder.DeleteData(
                table: "Taxes",
                keyColumn: "Id",
                keyValue: new Guid("0b6bd721-a40d-43e2-affc-de1646f85f8b"));

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: new Guid("7abeec0a-8840-4e72-af65-94989aa32c9d"));

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("0237ecf2-85e3-4873-bef6-78ae56322cc4"));

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("15a6343e-1780-4a41-b730-dd7e4c75eb55"));

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("4e8949ec-404c-4e5f-a47e-0898f8800a1e"));

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("5cae9352-4511-40eb-a7f4-de0fdb9c2cbb"));

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("6f01eab3-9be3-497d-b7ec-6a311bdc2b34"));

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("70a1b51c-d009-44d0-ad08-c43cf5d0ab15"));

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("79ccbdb8-a022-4e3b-915f-974928cdf363"));

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("7bc038f9-80bf-46dc-8c29-8c94b732baf8"));

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("93d4df2d-038c-4a6a-bc76-035b019fe62e"));

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("9acb9c67-db21-4eb5-85b1-b7c189171b14"));

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("a62e1436-9533-4635-b3ec-6a072db2031a"));

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("bcfbd6fa-89be-45f9-956d-8ad2f40c3cfa"));

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("be251ce7-fb8b-49e7-b178-97297ca4c59c"));

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("ff5c38d8-5662-4d97-b093-9b8e0f53c057"));

            migrationBuilder.DeleteData(
                table: "Taxes",
                keyColumn: "Id",
                keyValue: new Guid("024f2996-eba3-4c83-aa42-78536c8d00a1"));

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "Id",
                keyValue: new Guid("99961c57-3a5b-440a-86e1-a5aef4ae33e1"));

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "Id",
                keyValue: new Guid("a6c28835-277e-44f1-878e-d7369c4993dd"));

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "Id",
                keyValue: new Guid("f2c82559-be2d-4aa3-ad88-3db28eaf3d34"));

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "Id",
                keyValue: new Guid("08030bd5-b032-42d1-87ab-42141e1e484c"));

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "Id",
                keyValue: new Guid("67ec6d8e-eb94-4c6e-9a7a-6a0995895d70"));

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "Id",
                keyValue: new Guid("6aaa9938-7900-4316-9c5c-9621c410cd52"));

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "Id",
                keyValue: new Guid("8c73349b-2c71-47fd-9bbe-1cbb81f71f48"));

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "Id",
                keyValue: new Guid("c17a3c19-7420-4f75-bc15-cad8cc100803"));

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "Id",
                keyValue: new Guid("d88685cb-278e-4cee-814e-241955c1cd24"));

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "Id",
                keyValue: new Guid("fe76ac24-c8b6-4d68-9f2a-4b86893e0ad5"));

            migrationBuilder.RenameTable(
                name: "FlightDiscount",
                newName: "DiscountFlight");

            migrationBuilder.RenameIndex(
                name: "IX_FlightDiscount_FlightsId",
                table: "DiscountFlight",
                newName: "IX_DiscountFlight_FlightsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscountFlight",
                table: "DiscountFlight",
                columns: new[] { "DiscountsId", "FlightsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountFlight_Discounts_DiscountsId",
                table: "DiscountFlight",
                column: "DiscountsId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountFlight_Flights_FlightsId",
                table: "DiscountFlight",
                column: "FlightsId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

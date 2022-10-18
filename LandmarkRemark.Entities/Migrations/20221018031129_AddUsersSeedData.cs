using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandmarkRemark.Entities.Migrations
{
    public partial class AddUsersSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created", "Deleted", "EmailAddress", "FirstName", "LastName", "Password", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 18, 12, 45, 0, 0, DateTimeKind.Unspecified), false, "anthony.albanese@example.com", "Anthony", "Albanese", "anthonyalbanese", new DateTime(2022, 10, 18, 12, 45, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2022, 10, 18, 12, 45, 0, 0, DateTimeKind.Unspecified), false, "richard.marles@example.com", "Richard", "Marles", "richardmarles", new DateTime(2022, 10, 18, 12, 45, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2022, 10, 18, 12, 45, 0, 0, DateTimeKind.Unspecified), false, "jim.chalmers@example.com", "Jim", "Chalmers", "jimchalmers", new DateTime(2022, 10, 18, 12, 45, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2022, 10, 18, 12, 45, 0, 0, DateTimeKind.Unspecified), false, "penny.wong@example.com", "Penny", "Wong", "pennywong", new DateTime(2022, 10, 18, 12, 45, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2022, 10, 18, 12, 45, 0, 0, DateTimeKind.Unspecified), false, "mark.butler@example.com", "Mark", "Butler", "markbulter", new DateTime(2022, 10, 18, 12, 45, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}

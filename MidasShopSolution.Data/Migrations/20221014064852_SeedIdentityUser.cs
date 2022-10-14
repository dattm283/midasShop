using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MidasShopSolution.Data.Migrations
{
    public partial class SeedIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 14, 13, 48, 51, 412, DateTimeKind.Local).AddTicks(945),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 14, 13, 37, 36, 995, DateTimeKind.Local).AddTicks(2738));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 10, 14, 13, 48, 51, 429, DateTimeKind.Local).AddTicks(6702));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("8256ee14-c826-4e80-83de-d467eb4ee1e7"), "53298b47-ad5a-48d3-8b3c-ff3987058832", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("8256ee14-c826-4e80-83de-d467eb4ee1e7"), new Guid("84d35d2a-8eef-4497-95f7-3b32bdf64546") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("84d35d2a-8eef-4497-95f7-3b32bdf64546"), 0, "4c1a3a7d-82e0-46e8-a522-c4c99a2d82b1", new DateTime(2001, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "dattranadmin@gmail.com", true, "Dat", "Tran", false, null, "dattranadmin@gmail.com", "admin", "AQAAAAEAACcQAAAAEEHcg9GX5uzBh0iaDLP9cnmuBFVV7fCDAiUCcBKRkFJ2qIDesORGM94XvYRHWj1aVg==", null, false, "", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8256ee14-c826-4e80-83de-d467eb4ee1e7"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("8256ee14-c826-4e80-83de-d467eb4ee1e7"), new Guid("84d35d2a-8eef-4497-95f7-3b32bdf64546") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("84d35d2a-8eef-4497-95f7-3b32bdf64546"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 14, 13, 37, 36, 995, DateTimeKind.Local).AddTicks(2738),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 14, 13, 48, 51, 412, DateTimeKind.Local).AddTicks(945));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 10, 14, 13, 37, 36, 999, DateTimeKind.Local).AddTicks(4891));
        }
    }
}

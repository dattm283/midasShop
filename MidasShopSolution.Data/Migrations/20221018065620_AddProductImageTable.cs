using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MidasShopSolution.Data.Migrations
{
    public partial class AddProductImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 14, 13, 48, 51, 412, DateTimeKind.Local).AddTicks(945));

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 10, 18, 13, 56, 19, 401, DateTimeKind.Local).AddTicks(9476));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8256ee14-c826-4e80-83de-d467eb4ee1e7"),
                column: "ConcurrencyStamp",
                value: "d19c6591-1cf3-4391-ae1e-48a10b196e90");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("84d35d2a-8eef-4497-95f7-3b32bdf64546"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cd6aac90-a057-4ed5-a7ea-7a5aef7a4cc7", "AQAAAAEAACcQAAAAECrJt9/QDgMer+d5X2jInnLIG/kYFIW8RULuihnDHiRUykRaP8HGfq28eXx1q4wQVw==" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 14, 13, 48, 51, 412, DateTimeKind.Local).AddTicks(945),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 10, 14, 13, 48, 51, 429, DateTimeKind.Local).AddTicks(6702));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8256ee14-c826-4e80-83de-d467eb4ee1e7"),
                column: "ConcurrencyStamp",
                value: "53298b47-ad5a-48d3-8b3c-ff3987058832");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("84d35d2a-8eef-4497-95f7-3b32bdf64546"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4c1a3a7d-82e0-46e8-a522-c4c99a2d82b1", "AQAAAAEAACcQAAAAEEHcg9GX5uzBh0iaDLP9cnmuBFVV7fCDAiUCcBKRkFJ2qIDesORGM94XvYRHWj1aVg==" });
        }
    }
}

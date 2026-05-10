using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RetailSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEntityColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Colors_ColorId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Colors_ColorId",
                table: "ProductVariants");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_ColorId",
                table: "ProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ColorId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "ProductImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ColorId",
                table: "ProductVariants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ColorId",
                table: "ProductImages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "ColorCode", "ColorName" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "#000000", "Black" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "#FFFFFF", "White" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "#808080", "Grey" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "#FF0000", "Red" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "#000080", "Navy Blue" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "#008000", "Green" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "#FFFF00", "Yellow" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "#FFA500", "Orange" },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "#A52A2A", "Brown" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "#FFC0CB", "Pink" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "#800080", "Purple" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "#F5F5DC", "Beige" },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "#FFFDD0", "Cream" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "#C0C0C0", "Silver" },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), "#D4AF37", "Gold" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ColorId",
                table: "ProductVariants",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ColorId",
                table: "ProductImages",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Colors_ColorId",
                table: "ProductImages",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Colors_ColorId",
                table: "ProductVariants",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstate.Repositories.Migrations
{
    public partial class addedImageToAgent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Agent",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agent_ImageId",
                table: "Agent",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_Image_ImageId",
                table: "Agent",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agent_Image_ImageId",
                table: "Agent");

            migrationBuilder.DropIndex(
                name: "IX_Agent_ImageId",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Agent");
        }
    }
}

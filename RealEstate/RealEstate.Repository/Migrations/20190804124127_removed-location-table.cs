using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstate.Repositories.Migrations
{
    public partial class removedlocationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_Location_LocationId",
                table: "Property");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Property",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_LocationId",
                table: "Property",
                newName: "IX_Property_CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_City_CityId",
                table: "Property",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_City_CityId",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Property",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_CityId",
                table: "Property",
                newName: "IX_Property_LocationId");

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_CityId",
                table: "Location",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Location_LocationId",
                table: "Property",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

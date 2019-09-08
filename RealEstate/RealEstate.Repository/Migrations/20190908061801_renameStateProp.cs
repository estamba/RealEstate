using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstate.Repositories.Migrations
{
    public partial class renameStateProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_PropertyState_StateId",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "SateId",
                table: "Property");

            migrationBuilder.AlterColumn<short>(
                name: "StateId",
                table: "Property",
                nullable: false,
                oldClrType: typeof(short),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_PropertyState_StateId",
                table: "Property",
                column: "StateId",
                principalTable: "PropertyState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_PropertyState_StateId",
                table: "Property");

            migrationBuilder.AlterColumn<short>(
                name: "StateId",
                table: "Property",
                nullable: true,
                oldClrType: typeof(short));

            migrationBuilder.AddColumn<short>(
                name: "SateId",
                table: "Property",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_PropertyState_StateId",
                table: "Property",
                column: "StateId",
                principalTable: "PropertyState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

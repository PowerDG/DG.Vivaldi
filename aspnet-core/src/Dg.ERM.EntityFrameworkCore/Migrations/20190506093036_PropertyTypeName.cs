using Microsoft.EntityFrameworkCore.Migrations;

namespace Dg.ERM.Migrations
{
    public partial class PropertyTypeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EnityID",
                table: "ExtendInfos",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "PropertyName",
                table: "ExtendInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyTypeName",
                table: "ExtendInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyValue",
                table: "ExtendInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnityID",
                table: "ExtendInfos");

            migrationBuilder.DropColumn(
                name: "PropertyName",
                table: "ExtendInfos");

            migrationBuilder.DropColumn(
                name: "PropertyTypeName",
                table: "ExtendInfos");

            migrationBuilder.DropColumn(
                name: "PropertyValue",
                table: "ExtendInfos");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Dg.ERM.Migrations
{
    public partial class PropertyTypeName2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                table: "ExtendInfos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExtendInfos",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                table: "ExtendInfos",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExtendInfos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

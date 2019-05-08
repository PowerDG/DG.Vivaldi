using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Dg.ERM.Migrations
{
    public partial class InitExtendInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtendInfos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TenantId = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: false),
                    Super_Type = table.Column<string>(nullable: true),
                    EntityTypeFullName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    DataTypeName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtendInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtendInfos");
        }
    }
}

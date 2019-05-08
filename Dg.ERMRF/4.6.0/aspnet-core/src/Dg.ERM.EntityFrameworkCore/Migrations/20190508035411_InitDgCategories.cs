using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Dg.ERM.Migrations
{
    public partial class InitDgCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DgCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ParentId = table.Column<long>(nullable: true),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Image = table.Column<string>(maxLength: 255, nullable: true),
                    Content = table.Column<string>(maxLength: 4000, nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DgCategories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DgCategories");
        }
    }
}

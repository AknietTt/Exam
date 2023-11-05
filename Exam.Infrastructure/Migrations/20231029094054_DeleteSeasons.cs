using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteSeasons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Seasons_SeasonId",
                schema: "public",
                table: "Leagues");

            migrationBuilder.DropTable(
                name: "Seasons",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_Leagues_SeasonId",
                schema: "public",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "SeasonId",
                schema: "public",
                table: "Leagues");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SeasonId",
                schema: "public",
                table: "Leagues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Seasons",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SystemId = table.Column<long>(type: "bigint", nullable: false),
                    Year = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_SeasonId",
                schema: "public",
                table: "Leagues",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_Seasons_SeasonId",
                schema: "public",
                table: "Leagues",
                column: "SeasonId",
                principalSchema: "public",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

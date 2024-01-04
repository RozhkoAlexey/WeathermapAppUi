using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeathermapApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryQueries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Weather = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryQueries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryQueries_ZipCode_CountryCode",
                table: "HistoryQueries",
                columns: new[] { "ZipCode", "CountryCode" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryQueries");
        }
    }
}

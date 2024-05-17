using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify_Connector.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArtistCode = table.Column<string>(type: "varchar(150)", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", nullable: false),
                    Uri = table.Column<string>(type: "varchar(100)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "Datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "Datetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "Datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artist");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuvekkilTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class yeni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "Odeme_Sekli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odeme_Sekli", x => x.Id);
                });
        }

        /// <inheritdoc />
       
    }
}

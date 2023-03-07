using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NavalWar.DAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Map",
                columns: table => new
                {
                    IdMap = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Column = table.Column<int>(type: "int", nullable: false),
                    Line = table.Column<int>(type: "int", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListTarget = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    idPlayer = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Map", x => x.IdMap);
                    table.ForeignKey(
                        name: "FK_Map_Player_idPlayer",
                        column: x => x.idPlayer,
                        principalTable: "Player",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    IdGame = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idMap0 = table.Column<int>(type: "int", nullable: true),
                    idMap1 = table.Column<int>(type: "int", nullable: true),
                    Result = table.Column<int>(type: "int", nullable: false),
                    WinnerId = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.IdGame);
                    table.ForeignKey(
                        name: "FK_Game_Map_idMap0",
                        column: x => x.idMap0,
                        principalTable: "Map",
                        principalColumn: "IdMap");
                    table.ForeignKey(
                        name: "FK_Game_Map_idMap1",
                        column: x => x.idMap1,
                        principalTable: "Map",
                        principalColumn: "IdMap");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_idMap0",
                table: "Game",
                column: "idMap0");

            migrationBuilder.CreateIndex(
                name: "IX_Game_idMap1",
                table: "Game",
                column: "idMap1");

            migrationBuilder.CreateIndex(
                name: "IX_Map_idPlayer",
                table: "Map",
                column: "idPlayer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Map");

            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}

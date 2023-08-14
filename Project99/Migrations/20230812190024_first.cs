using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project99.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbCargo",
                columns: table => new
                {
                    IdCargo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbCargo", x => x.IdCargo);
                });

            migrationBuilder.CreateTable(
                name: "TbIntegrantes",
                columns: table => new
                {
                    IdIntegrantes = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apelido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    IdCargo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbIntegrantes", x => x.IdIntegrantes);
                    table.ForeignKey(
                        name: "FK_TbIntegrantes_TbCargo_IdCargo",
                        column: x => x.IdCargo,
                        principalTable: "TbCargo",
                        principalColumn: "IdCargo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbIntegrantes_IdCargo",
                table: "TbIntegrantes",
                column: "IdCargo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbIntegrantes");

            migrationBuilder.DropTable(
                name: "TbCargo");
        }
    }
}

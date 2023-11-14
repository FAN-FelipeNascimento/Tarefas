using Microsoft.EntityFrameworkCore.Migrations;

namespace Tarefas.Migrations
{
    public partial class CriandoBaseDados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "objTbTarefas",
                columns: table => new
                {
                    IdTarefa = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_objTbTarefas", x => x.IdTarefa);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "objTbTarefas");
        }
    }
}

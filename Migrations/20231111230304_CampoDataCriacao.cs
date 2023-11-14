using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tarefas.Migrations
{
    public partial class CampoDataCriacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DtLogCriacao",
                table: "objTbTarefas",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DtLogCriacao",
                table: "objTbTarefas");
        }
    }
}

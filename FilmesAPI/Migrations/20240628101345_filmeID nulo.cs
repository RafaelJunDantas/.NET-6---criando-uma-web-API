using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmesAPI.Migrations
{
    public partial class filmeIDnulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Filmes_filmeID",
                table: "Sessoes");

            migrationBuilder.AlterColumn<int>(
                name: "filmeID",
                table: "Sessoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Filmes_filmeID",
                table: "Sessoes",
                column: "filmeID",
                principalTable: "Filmes",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Filmes_filmeID",
                table: "Sessoes");

            migrationBuilder.AlterColumn<int>(
                name: "filmeID",
                table: "Sessoes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Filmes_filmeID",
                table: "Sessoes",
                column: "filmeID",
                principalTable: "Filmes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmesAPI.Migrations
{
    public partial class fixerrors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Cinemas_cinemaID",
                table: "Sessoes");

            migrationBuilder.AlterColumn<int>(
                name: "cinemaID",
                table: "Sessoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Cinemas_cinemaID",
                table: "Sessoes",
                column: "cinemaID",
                principalTable: "Cinemas",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Cinemas_cinemaID",
                table: "Sessoes");

            migrationBuilder.AlterColumn<int>(
                name: "cinemaID",
                table: "Sessoes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Cinemas_cinemaID",
                table: "Sessoes",
                column: "cinemaID",
                principalTable: "Cinemas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace modelado1.Comunes.Migrations
{
    public partial class inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCurso = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    TutorCurso = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Resumen = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaCursado = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Aula = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    CursoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Horarios_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_Curso_NombreCurso",
                table: "Cursos",
                column: "NombreCurso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Horarios_CursoId",
                table: "Horarios",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "UQ_Horaio_DiaCursado",
                table: "Horarios",
                column: "DiaCursado",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.DropTable(
                name: "Cursos");
        }
    }
}

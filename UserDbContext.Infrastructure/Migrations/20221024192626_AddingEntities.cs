using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserDbContext.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreGrado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Habilidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreHabilidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionHabilidad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habilidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instructores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdGradoAcademico = table.Column<int>(type: "int", nullable: false),
                    Perfil = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructores_Grados_IdGradoAcademico",
                        column: x => x.IdGradoAcademico,
                        principalTable: "Grados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instructores_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HabilidadesInstructores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdHabilidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabilidadesInstructores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabilidadesInstructores_Habilidades_IdHabilidad",
                        column: x => x.IdHabilidad,
                        principalTable: "Habilidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HabilidadesInstructores_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HabilidadesInstructores_IdHabilidad",
                table: "HabilidadesInstructores",
                column: "IdHabilidad");

            migrationBuilder.CreateIndex(
                name: "IX_HabilidadesInstructores_IdUsuario",
                table: "HabilidadesInstructores",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Instructores_IdGradoAcademico",
                table: "Instructores",
                column: "IdGradoAcademico");

            migrationBuilder.CreateIndex(
                name: "IX_Instructores_IdUsuario",
                table: "Instructores",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HabilidadesInstructores");

            migrationBuilder.DropTable(
                name: "Instructores");

            migrationBuilder.DropTable(
                name: "Habilidades");

            migrationBuilder.DropTable(
                name: "Grados");
        }
    }
}

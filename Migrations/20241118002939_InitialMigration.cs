using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SickLeaveAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CobrosJuridicos",
                columns: table => new
                {
                    IdCobro = table.Column<Guid>(type: "uuid", nullable: false),
                    CedulaAbogado = table.Column<long>(type: "bigint", nullable: false),
                    IdIncapacidad = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEpsArl = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaCobro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DerechoPeticion = table.Column<string>(type: "text", nullable: false),
                    Tutela = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CobrosJuridicos", x => x.IdCobro);
                });

            migrationBuilder.CreateTable(
                name: "EPS_ARLs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NumeroContacto = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EPS_ARLs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Incapacidades",
                columns: table => new
                {
                    IDIncapacidad = table.Column<Guid>(type: "uuid", nullable: false),
                    CedulaColaborador = table.Column<long>(type: "bigint", nullable: false),
                    IdEpsArl = table.Column<Guid>(type: "uuid", nullable: false),
                    CedulaRH = table.Column<long>(type: "bigint", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Tipo = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Estado = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    DocumentoAdjunto = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incapacidades", x => x.IDIncapacidad);
                });

            migrationBuilder.CreateTable(
                name: "PagosIncapacidades",
                columns: table => new
                {
                    IdPago = table.Column<Guid>(type: "uuid", nullable: false),
                    CedulaTesorero = table.Column<long>(type: "bigint", nullable: false),
                    IdIncapacidad = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaRadicacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<int>(type: "integer", maxLength: 20, nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagosIncapacidades", x => x.IdPago);
                });

            migrationBuilder.CreateTable(
                name: "UserAbogados",
                columns: table => new
                {
                    Cedula = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CedulaAdministrador = table.Column<long>(type: "bigint", nullable: false),
                    PrimerNombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SegundoNombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PrimerApellido = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SegundoApellido = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    FechaDeCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAbogados", x => x.Cedula);
                });

            migrationBuilder.CreateTable(
                name: "UserAdmins",
                columns: table => new
                {
                    Cedula = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrimerNombre = table.Column<string>(type: "text", nullable: false),
                    SegundoNombre = table.Column<string>(type: "text", nullable: true),
                    PrimerApellido = table.Column<string>(type: "text", nullable: false),
                    SegundoApellido = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdmins", x => x.Cedula);
                });

            migrationBuilder.CreateTable(
                name: "UserColaboradors",
                columns: table => new
                {
                    Cedula = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CedulaAdministrador = table.Column<long>(type: "bigint", nullable: false),
                    PrimerNombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SegundoNombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PrimerApellido = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SegundoApellido = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Rol = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    FechaDeCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserColaboradors", x => x.Cedula);
                });

            migrationBuilder.CreateTable(
                name: "UserRHs",
                columns: table => new
                {
                    Cedula = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CedulaAdministrador = table.Column<long>(type: "bigint", nullable: false),
                    PrimerNombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SegundoNombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PrimerApellido = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SegundoApellido = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    FechaDeCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRHs", x => x.Cedula);
                });

            migrationBuilder.CreateTable(
                name: "UserTesoreros",
                columns: table => new
                {
                    Cedula = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CedulaAdministrador = table.Column<long>(type: "bigint", nullable: false),
                    PrimerNombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SegundoNombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PrimerApellido = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SegundoApellido = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    FechaDeCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTesoreros", x => x.Cedula);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CobrosJuridicos");

            migrationBuilder.DropTable(
                name: "EPS_ARLs");

            migrationBuilder.DropTable(
                name: "Incapacidades");

            migrationBuilder.DropTable(
                name: "PagosIncapacidades");

            migrationBuilder.DropTable(
                name: "UserAbogados");

            migrationBuilder.DropTable(
                name: "UserAdmins");

            migrationBuilder.DropTable(
                name: "UserColaboradors");

            migrationBuilder.DropTable(
                name: "UserRHs");

            migrationBuilder.DropTable(
                name: "UserTesoreros");
        }
    }
}

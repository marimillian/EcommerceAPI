using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuariosApi.Migrations
{
    public partial class Atualizacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99995,
                column: "ConcurrencyStamp",
                value: "dae736c9-706a-4b1f-9d57-6a369590a23d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "e3bf280b-7178-437d-b3b4-29d1898c2780");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "e976f287-c091-43c6-aa3a-60010dd2cbb0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99995,
                column: "ConcurrencyStamp",
                value: "ccb950a2-1584-43b4-9da1-abe1b1119a95");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "b4751c59-8802-4a7d-9ea8-cfebe92d0d2d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "46ebb60c-e3e3-4a71-8f11-9b02960d0a31");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bairro", "CEP", "CPF", "Complemento", "ConcurrencyStamp", "DataAtualizacao", "DataCadastro", "DataNascimento", "Email", "EmailConfirmed", "Localidade", "LockoutEnabled", "LockoutEnd", "Logradouro", "Nome", "NormalizedEmail", "NormalizedUserName", "Numero", "PasswordHash", "Perfil", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UF", "UserName" },
                values: new object[] { 99999, 0, "IMIRIM", "02468040", "87817936005", "CASA", "c9c74b2f-f70e-4903-9eea-88e4c4c590ad", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", true, "SÃO PAULO", false, null, "RUA IBIRATINGA", "ADMINISTRADOR", "ADMIN@ADMIN.COM", "ADMIN", 104, "AQAAAAEAACcQAAAAELsabFjYTym5yFQnf8hqjcwK1RdqkQ7c0+OTLx/vYqTsF5fah+dvUAjvngPhe2q6fw==", "ADMIN", null, false, "803186b3-0fe4-4e1c-b276-a837b2cee073", true, false, "SP", "admin" });
        }
    }
}

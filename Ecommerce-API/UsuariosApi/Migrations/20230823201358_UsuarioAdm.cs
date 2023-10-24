using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuariosApi.Migrations
{
    public partial class UsuarioAdm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99995,
                column: "ConcurrencyStamp",
                value: "082dc50e-e868-45d1-b478-52aa1b07eb32");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "b516666d-33c3-4e51-ad87-2fb28583f4c5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "ef8976e3-e35c-437a-bfa1-bffa6ac05e48");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bairro", "CEP", "CPF", "Complemento", "ConcurrencyStamp", "DataAtualizacao", "DataCadastro", "DataNascimento", "Email", "EmailConfirmed", "Localidade", "LockoutEnabled", "LockoutEnd", "Logradouro", "Nome", "NormalizedEmail", "NormalizedUserName", "Numero", "PasswordHash", "Perfil", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UF", "UserName" },
                values: new object[] { 99999, 0, "IMIRIM", "02468040", "87817936005", "CASA", "a6ebfbbe-e626-4e68-8bdf-2e08a89b13e0", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", true, "SÃO PAULO", false, null, "RUA IBIRATINGA", "ADMINISTRADOR", "ADMIN@ADMIN.COM", "ADMIN", 104, "AQAAAAEAACcQAAAAEPCzqdzNzcNS/sd3EKPeBZV/fPlQCB12xWhPj6F5Nd8RiaiCDZ10bB6ARRxIsfaBpg==", "ADMIN", null, false, "8c9eaed2-ff1a-44ee-a5a3-6b7e359303f3", true, false, "SP", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}

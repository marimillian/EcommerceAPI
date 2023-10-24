using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuariosApi.Migrations
{
    public partial class AdicionandoAdministrador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "Bairro", "CEP", "CPF", "Complemento", "ConcurrencyStamp", "Localidade", "Logradouro", "Nome", "Numero", "PasswordHash", "Perfil", "SecurityStamp", "Status", "UF" },
                values: new object[] { "IMIRIM", "02468040", "87817936005", "CASA", "c9c74b2f-f70e-4903-9eea-88e4c4c590ad", "SÃO PAULO", "RUA IBIRATINGA", "ADMINISTRADOR", 104, "AQAAAAEAACcQAAAAELsabFjYTym5yFQnf8hqjcwK1RdqkQ7c0+OTLx/vYqTsF5fah+dvUAjvngPhe2q6fw==", "ADMIN", "803186b3-0fe4-4e1c-b276-a837b2cee073", true, "SP" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99995,
                column: "ConcurrencyStamp",
                value: "a4b18708-da92-4613-a480-06f8628f9185");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "64c8064e-cee5-4141-9fb5-5640754217ee");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "3102cfb7-3769-4188-a9e2-28fcc56a9c4c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "Bairro", "CEP", "CPF", "Complemento", "ConcurrencyStamp", "Localidade", "Logradouro", "Nome", "Numero", "PasswordHash", "Perfil", "SecurityStamp", "Status", "UF" },
                values: new object[] { null, null, null, null, "40fd862e-665e-4cae-86ef-a027066da55f", null, null, null, 0, "AQAAAAEAACcQAAAAEDiYMn7ItvP+czyz0wLUEQxmgLapiCkB1cJasHt+j1dwClbSsUAOjVfNj0jrWbAojQ==", null, "f74f7187-7ea4-411d-9dd8-a204c273f492", false, null });
        }
    }
}

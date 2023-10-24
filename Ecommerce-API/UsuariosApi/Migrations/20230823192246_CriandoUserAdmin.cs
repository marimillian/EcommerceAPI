using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuariosApi.Migrations
{
    public partial class CriandoUserAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40fd862e-665e-4cae-86ef-a027066da55f", "AQAAAAEAACcQAAAAEDiYMn7ItvP+czyz0wLUEQxmgLapiCkB1cJasHt+j1dwClbSsUAOjVfNj0jrWbAojQ==", "f74f7187-7ea4-411d-9dd8-a204c273f492" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99995,
                column: "ConcurrencyStamp",
                value: "b4524a9a-69d2-42b9-8f19-dee040dee114");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "cc06438d-4a26-4659-a284-695dfe18c85c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "f079a89f-0c8c-4150-b7dd-6e7348ec2fce");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c9e35e26-5b03-41c5-8bc9-31b89980e262", "AQAAAAEAACcQAAAAEHJB5K7RnBSm7hZFPdpoM2o3ZtAh8sqFoSKLdXlOXvoO4kleH/nZmwtXahybZ2UqyQ==", "d6312620-2ef4-4518-9b9e-13b12c4463e2" });
        }
    }
}

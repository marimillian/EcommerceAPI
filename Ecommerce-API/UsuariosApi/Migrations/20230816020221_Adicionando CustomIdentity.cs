using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuariosApi.Migrations
{
    public partial class AdicionandoCustomIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99995,
                column: "ConcurrencyStamp",
                value: "601b5a18-735c-4fbc-a293-5bf66622882d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "195dd158-3f60-46aa-bdfa-30fb0ab3345b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "6755ac23-7263-4823-b1c7-d864f6ac41b8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "047742bd-49b9-4b9e-997c-e2980aa73c21", "AQAAAAEAACcQAAAAEGFOarNB+6ci1RBQ1uAATz5PDJ3KJ12WM4KeUxCaBg4uRY/hoLShMGP/zqg6GAJsmw==", "bb79bedd-a837-46f3-842f-88a7671bcc69" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TokonyadiaApi.Migrations
{
    /// <inheritdoc />
    public partial class RoleCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "m_user_credential",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_m_user_credential_email",
                table: "m_user_credential",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_m_user_credential_email",
                table: "m_user_credential");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "m_user_credential",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

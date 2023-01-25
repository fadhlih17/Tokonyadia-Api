using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TokonyadiaApi.Migrations
{
    /// <inheritdoc />
    public partial class UserCredential : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_m_customer_email",
                table: "m_customer");

            migrationBuilder.DropColumn(
                name: "email",
                table: "m_customer");

            migrationBuilder.AddColumn<Guid>(
                name: "user_credential_id",
                table: "m_customer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "m_user_credential",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_user_credential", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_customer_user_credential_id",
                table: "m_customer",
                column: "user_credential_id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_customer_m_user_credential_user_credential_id",
                table: "m_customer",
                column: "user_credential_id",
                principalTable: "m_user_credential",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_customer_m_user_credential_user_credential_id",
                table: "m_customer");

            migrationBuilder.DropTable(
                name: "m_user_credential");

            migrationBuilder.DropIndex(
                name: "IX_m_customer_user_credential_id",
                table: "m_customer");

            migrationBuilder.DropColumn(
                name: "user_credential_id",
                table: "m_customer");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "m_customer",
                type: "NVarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_m_customer_email",
                table: "m_customer",
                column: "email",
                unique: true);
        }
    }
}

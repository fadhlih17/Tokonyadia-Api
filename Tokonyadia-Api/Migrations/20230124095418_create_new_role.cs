using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TokonyadiaApi.Migrations
{
    /// <inheritdoc />
    public partial class createnewrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "role",
                table: "m_user_credential");

            migrationBuilder.AddColumn<Guid>(
                name: "role_id",
                table: "m_user_credential",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "m_role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_role", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_user_credential_role_id",
                table: "m_user_credential",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_m_user_credential_m_role_role_id",
                table: "m_user_credential",
                column: "role_id",
                principalTable: "m_role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_user_credential_m_role_role_id",
                table: "m_user_credential");

            migrationBuilder.DropTable(
                name: "m_role");

            migrationBuilder.DropIndex(
                name: "IX_m_user_credential_role_id",
                table: "m_user_credential");

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "m_user_credential");

            migrationBuilder.AddColumn<int>(
                name: "role",
                table: "m_user_credential",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

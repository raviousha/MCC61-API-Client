using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addcascade1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_tr_AccountRoles_Tb_m_Roles_RolesId",
                table: "Tb_tr_AccountRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_tr_AccountRoles_Tb_m_Roles_RolesId",
                table: "Tb_tr_AccountRoles",
                column: "RolesId",
                principalTable: "Tb_m_Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_tr_AccountRoles_Tb_m_Roles_RolesId",
                table: "Tb_tr_AccountRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_tr_AccountRoles_Tb_m_Roles_RolesId",
                table: "Tb_tr_AccountRoles",
                column: "RolesId",
                principalTable: "Tb_m_Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

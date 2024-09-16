using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoenixTask.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class updatemembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Role_RoleId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaceMember_User_UserId",
                table: "WorkSpaceMember");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaceMember_WorkSpace_WorkSpaceId",
                table: "WorkSpaceMember");

            migrationBuilder.DropTable(
                name: "ProjectMemberRole");

            migrationBuilder.DropTable(
                name: "RoleWorkSpaceMember");

            migrationBuilder.DropTable(
                name: "ProjectMember");

            migrationBuilder.DropIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkSpaceMember",
                table: "WorkSpaceMember");

            migrationBuilder.DropIndex(
                name: "IX_WorkSpaceMember_WorkSpaceId",
                table: "WorkSpaceMember");

            migrationBuilder.RenameTable(
                name: "WorkSpaceMember",
                newName: "Member");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "RolePermission",
                newName: "RoleValue");

            migrationBuilder.RenameIndex(
                name: "IX_WorkSpaceMember_UserId",
                table: "Member",
                newName: "IX_Member_UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkSpaceId",
                table: "Member",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "MemberType",
                table: "Member",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Member",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleValue",
                table: "Member",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Member",
                table: "Member",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Member_RoleValue",
                table: "Member",
                column: "RoleValue");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Role_RoleValue",
                table: "Member",
                column: "RoleValue",
                principalTable: "Role",
                principalColumn: "Value",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_User_UserId",
                table: "Member",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Role_RoleValue",
                table: "RolePermission",
                column: "RoleValue",
                principalTable: "Role",
                principalColumn: "Value",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_Role_RoleValue",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_User_UserId",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Role_RoleValue",
                table: "RolePermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Member",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Member_RoleValue",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "MemberType",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "RoleValue",
                table: "Member");

            migrationBuilder.RenameTable(
                name: "Member",
                newName: "WorkSpaceMember");

            migrationBuilder.RenameColumn(
                name: "RoleValue",
                table: "RolePermission",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Member_UserId",
                table: "WorkSpaceMember",
                newName: "IX_WorkSpaceMember_UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkSpaceId",
                table: "WorkSpaceMember",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkSpaceMember",
                table: "WorkSpaceMember",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProjectMember",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMember_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectMember_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleWorkSpaceMember",
                columns: table => new
                {
                    RolesValue = table.Column<int>(type: "int", nullable: false),
                    WorkSpaceMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleWorkSpaceMember", x => new { x.RolesValue, x.WorkSpaceMemberId });
                    table.ForeignKey(
                        name: "FK_RoleWorkSpaceMember_Role_RolesValue",
                        column: x => x.RolesValue,
                        principalTable: "Role",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleWorkSpaceMember_WorkSpaceMember_WorkSpaceMemberId",
                        column: x => x.WorkSpaceMemberId,
                        principalTable: "WorkSpaceMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMemberRole",
                columns: table => new
                {
                    ProjectMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolesValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMemberRole", x => new { x.ProjectMemberId, x.RolesValue });
                    table.ForeignKey(
                        name: "FK_ProjectMemberRole_ProjectMember_ProjectMemberId",
                        column: x => x.ProjectMemberId,
                        principalTable: "ProjectMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectMemberRole_Role_RolesValue",
                        column: x => x.RolesValue,
                        principalTable: "Role",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceMember_WorkSpaceId",
                table: "WorkSpaceMember",
                column: "WorkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMember_ProjectId",
                table: "ProjectMember",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMember_UserId",
                table: "ProjectMember",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMemberRole_RolesValue",
                table: "ProjectMemberRole",
                column: "RolesValue");

            migrationBuilder.CreateIndex(
                name: "IX_RoleWorkSpaceMember_WorkSpaceMemberId",
                table: "RoleWorkSpaceMember",
                column: "WorkSpaceMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                table: "RolePermission",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Role_RoleId",
                table: "RolePermission",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Value",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaceMember_User_UserId",
                table: "WorkSpaceMember",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaceMember_WorkSpace_WorkSpaceId",
                table: "WorkSpaceMember",
                column: "WorkSpaceId",
                principalTable: "WorkSpace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

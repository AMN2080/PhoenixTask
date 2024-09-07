using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoenixTask.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.CreateTable(
                name: "ProjectMember",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
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
                name: "WorkSpaceMember",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkSpaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSpaceMember_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkSpaceMember_WorkSpace_WorkSpaceId",
                        column: x => x.WorkSpaceId,
                        principalTable: "WorkSpace",
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

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceMember_UserId",
                table: "WorkSpaceMember",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceMember_WorkSpaceId",
                table: "WorkSpaceMember",
                column: "WorkSpaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectMemberRole");

            migrationBuilder.DropTable(
                name: "RoleWorkSpaceMember");

            migrationBuilder.DropTable(
                name: "ProjectMember");

            migrationBuilder.DropTable(
                name: "WorkSpaceMember");

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RoleValue = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RoleValue, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Role_RoleValue",
                        column: x => x.RoleValue,
                        principalTable: "Role",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");
        }
    }
}

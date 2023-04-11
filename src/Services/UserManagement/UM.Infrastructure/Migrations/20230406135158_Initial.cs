using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    approval_status_id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    modified_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_user_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "user_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_user_statuses_approval_status_id",
                        column: x => x.approval_status_id,
                        principalTable: "user_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "superadmin" },
                    { 2, "user" }
                });

            migrationBuilder.InsertData(
                table: "user_statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Waiting for approval" },
                    { 2, "Approved" },
                    { 3, "Rejected" },
                    { 4, "Deactivated" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_approval_status_id",
                table: "users",
                column: "approval_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "user_statuses");
        }
    }
}

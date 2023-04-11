using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UM.Infrastructure.Migrations.IntegrationEventLogger
{
    /// <inheritdoc />
    public partial class IntegrationEventLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "integration_event_states",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_integration_event_states", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "integration_event_logs",
                columns: table => new
                {
                    event_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    event_type_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state_id = table.Column<int>(type: "int", nullable: false),
                    times_sent = table.Column<int>(type: "int", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    transaction_id = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_integration_event_logs", x => x.event_id);
                    table.ForeignKey(
                        name: "FK_integration_event_logs_integration_event_states_state_id",
                        column: x => x.state_id,
                        principalTable: "integration_event_states",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "integration_event_states",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "notpublished" },
                    { 2, "inprogress" },
                    { 3, "published" },
                    { 4, "publishedfailed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_integration_event_logs_state_id",
                table: "integration_event_logs",
                column: "state_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "integration_event_logs");

            migrationBuilder.DropTable(
                name: "integration_event_states");
        }
    }
}

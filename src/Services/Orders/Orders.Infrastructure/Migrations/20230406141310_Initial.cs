using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "order_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment_methods",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_methods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "shipping_methods",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipping_methods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "weight_units",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weight_units", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    number = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    buyer = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    status_id = table.Column<int>(type: "int", nullable: false),
                    payment_method_id = table.Column<int>(type: "int", nullable: false),
                    shipping_method_id = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    weight = table.Column<int>(type: "int", nullable: false),
                    weight_unit_id = table.Column<int>(type: "int", nullable: false),
                    is_draft = table.Column<bool>(type: "bit", nullable: false),
                    street = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    city = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    state = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    country = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    zip_code = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    created_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    modified_by = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    modified_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_order_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "order_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_payment_methods_payment_method_id",
                        column: x => x.payment_method_id,
                        principalTable: "payment_methods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_shipping_methods_shipping_method_id",
                        column: x => x.shipping_method_id,
                        principalTable: "shipping_methods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_weight_units_weight_unit_id",
                        column: x => x.weight_unit_id,
                        principalTable: "weight_units",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<long>(type: "bigint", nullable: false),
                    product_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    units = table.Column<byte>(type: "tinyint", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    created_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    modified_by = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    modified_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "order_statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Submitted" },
                    { 2, "Awaiting validation" },
                    { 3, "Stock confirmed" },
                    { 4, "Paid" },
                    { 5, "Shipped" },
                    { 6, "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "payment_methods",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Cash on delivery" },
                    { 2, "Debit card" },
                    { 3, "Credit card" }
                });

            migrationBuilder.InsertData(
                table: "shipping_methods",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Fedex" },
                    { 2, "UPS" },
                    { 3, "USPS" }
                });

            migrationBuilder.InsertData(
                table: "weight_units",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "g" },
                    { 2, "kg" },
                    { 3, "t" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_number",
                table: "orders",
                column: "number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_payment_method_id",
                table: "orders",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_shipping_method_id",
                table: "orders",
                column: "shipping_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_status_id",
                table: "orders",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_weight_unit_id",
                table: "orders",
                column: "weight_unit_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "order_statuses");

            migrationBuilder.DropTable(
                name: "payment_methods");

            migrationBuilder.DropTable(
                name: "shipping_methods");

            migrationBuilder.DropTable(
                name: "weight_units");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyInterests.API.Infrastructure.Migrations
{
    public partial class MigrationOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    role = table.Column<int>(type: "INTEGER", nullable: false),
                    phone_number = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "debts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    customer_id = table.Column<int>(type: "INTEGER", nullable: false),
                    negotiator_id = table.Column<int>(type: "INTEGER", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    original_value = table.Column<double>(type: "REAL", nullable: false),
                    due_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    parcel_qty = table.Column<int>(type: "INTEGER", nullable: false),
                    interest_type = table.Column<int>(type: "INTEGER", nullable: false),
                    interest_interval = table.Column<int>(type: "INTEGER", nullable: false),
                    interest_percentage = table.Column<double>(type: "REAL", nullable: false),
                    negotiator_comission_percent = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_debts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_debts_users_customer_id",
                        column: x => x.customer_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_debts_users_negotiator_id",
                        column: x => x.negotiator_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "debt_parcels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    debt_id = table.Column<int>(type: "INTEGER", nullable: false),
                    parcel = table.Column<int>(type: "INTEGER", nullable: false),
                    value = table.Column<double>(type: "REAL", nullable: false),
                    due_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_debt_parcels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_debt_parcels_debts_debt_id",
                        column: x => x.debt_id,
                        principalTable: "debts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_debt_parcels_debt_id",
                table: "debt_parcels",
                column: "debt_id");

            migrationBuilder.CreateIndex(
                name: "IX_debts_customer_id",
                table: "debts",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_debts_negotiator_id",
                table: "debts",
                column: "negotiator_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "debt_parcels");

            migrationBuilder.DropTable(
                name: "debts");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

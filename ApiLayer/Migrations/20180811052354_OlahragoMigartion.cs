using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Olahrago.ApiLayer.Migrations
{
    public partial class OlahragoMigartion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    username = table.Column<string>(maxLength: 20, nullable: false),
                    password = table.Column<string>(maxLength: 32, nullable: false),
                    account_type = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "court",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    playground_id = table.Column<long>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    court_type = table.Column<int>(nullable: false),
                    size_type = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<string>(maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(nullable: true),
                    updated_by = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_court", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "playground",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    account_id = table.Column<long>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    province = table.Column<string>(maxLength: 40, nullable: false),
                    regency = table.Column<string>(maxLength: 40, nullable: false),
                    district = table.Column<string>(maxLength: 40, nullable: false),
                    address = table.Column<string>(maxLength: 100, nullable: false),
                    contact = table.Column<string>(maxLength: 15, nullable: false),
                    location = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playground", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "owner",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    account_id = table.Column<long>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    email = table.Column<string>(maxLength: 50, nullable: false),
                    phone = table.Column<string>(maxLength: 15, nullable: false),
                    identity_type = table.Column<int>(nullable: true),
                    identity_number = table.Column<int>(nullable: true),
                    address = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_owner", x => x.account_id);
                    table.ForeignKey(
                        name: "owner_account_fk",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    account_id = table.Column<long>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    email = table.Column<string>(maxLength: 50, nullable: false),
                    phone = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.account_id);
                    table.ForeignKey(
                        name: "user_account_fk",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "court");

            migrationBuilder.DropTable(
                name: "owner");

            migrationBuilder.DropTable(
                name: "playground");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "account");
        }
    }
}

using GiveFreely.Common;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiveFreely.DataAccess.Migrations
{
    public partial class AddDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Affiliates",
                columns: table => new
                {
                    IdAffiliate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Affiliates", x => x.IdAffiliate);
                });

            migrationBuilder.CreateTable(
                name: "Commisions",
                columns: table => new
                {
                    IdCommusion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromCount = table.Column<int>(type: "int", nullable: false),
                    ToCount = table.Column<int>(type: "int", nullable: true),
                    Money = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commisions", x => x.IdCommusion);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    IdCustomer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAffiliate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.IdCustomer);
                    table.ForeignKey(
                        name: "FK_Customers_Affiliates_IdAffiliate",
                        column: x => x.IdAffiliate,
                        principalTable: "Affiliates",
                        principalColumn: "IdAffiliate",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_IdAffiliate",
                table: "Customers",
                column: "IdAffiliate");

            migrationBuilder.Sql($"INSERT INTO {DBTables.DBCommisions} (FromCount,ToCount,Money) Values (1, 100, 10)");
            migrationBuilder.Sql($"INSERT INTO {DBTables.DBCommisions} (FromCount,ToCount,Money) Values (100, 500, 15)");
            migrationBuilder.Sql($"INSERT INTO {DBTables.DBCommisions} (FromCount,ToCount,Money) Values (500,1000,20)");
            migrationBuilder.Sql($"INSERT INTO {DBTables.DBCommisions} (FromCount,Money) Values (1000,25)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commisions");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Affiliates");
        }
    }
}

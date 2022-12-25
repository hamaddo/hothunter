using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rgr.Migrations
{
    /// <inheritdoc />
    public partial class EmployersAndOffersInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    OwnerShipType = table.Column<int>(type: "integer", nullable: false),
                    RegistryNumber = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PositionName = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Salary = table.Column<int>(type: "integer", nullable: false),
                    EmployerId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Employers_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_EmployerId",
                table: "Offers",
                column: "EmployerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Employers");
        }
    }
}

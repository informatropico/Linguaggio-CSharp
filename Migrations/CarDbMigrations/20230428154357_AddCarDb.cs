using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinqQueries.Migrations.CarDbMigrations
{
    /// <inheritdoc />
    public partial class AddCarDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Manufacturer = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Displacement = table.Column<double>(type: "REAL", nullable: false),
                    Cylinders = table.Column<int>(type: "INTEGER", nullable: false),
                    City = table.Column<int>(type: "INTEGER", nullable: false),
                    Highway = table.Column<int>(type: "INTEGER", nullable: false),
                    Combined = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}

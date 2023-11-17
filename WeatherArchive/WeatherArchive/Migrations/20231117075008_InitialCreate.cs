using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WeatherArchive.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherPhenomenon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherPhenomenon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Humidity = table.Column<float>(type: "real", nullable: false),
                    DewPoint = table.Column<float>(type: "real", nullable: false),
                    Pressure = table.Column<float>(type: "real", nullable: false),
                    WindSpeed = table.Column<float>(type: "real", nullable: false),
                    Cloudiness = table.Column<float>(type: "real", nullable: false),
                    CloudBase = table.Column<float>(type: "real", nullable: false),
                    HorizontalVisibility = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherReport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WindDirection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WindDirection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherPhenomenonWeatherReport",
                columns: table => new
                {
                    WeatherPhenomenon = table.Column<int>(type: "integer", nullable: false),
                    WeatherReport = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherPhenomenonWeatherReport", x => new { x.WeatherPhenomenon, x.WeatherReport });
                    table.ForeignKey(
                        name: "FK_WeatherPhenomenonWeatherReport_WeatherPhenomenon_WeatherPhe~",
                        column: x => x.WeatherPhenomenon,
                        principalTable: "WeatherPhenomenon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeatherPhenomenonWeatherReport_WeatherReport_WeatherReport",
                        column: x => x.WeatherReport,
                        principalTable: "WeatherReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeatherReportWindDirection",
                columns: table => new
                {
                    WeatherReport = table.Column<int>(type: "integer", nullable: false),
                    WindDirection = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherReportWindDirection", x => new { x.WeatherReport, x.WindDirection });
                    table.ForeignKey(
                        name: "FK_WeatherReportWindDirection_WeatherReport_WeatherReport",
                        column: x => x.WeatherReport,
                        principalTable: "WeatherReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeatherReportWindDirection_WindDirection_WindDirection",
                        column: x => x.WindDirection,
                        principalTable: "WindDirection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherPhenomenonWeatherReport_WeatherReport",
                table: "WeatherPhenomenonWeatherReport",
                column: "WeatherReport");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherReportWindDirection_WindDirection",
                table: "WeatherReportWindDirection",
                column: "WindDirection");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherPhenomenonWeatherReport");

            migrationBuilder.DropTable(
                name: "WeatherReportWindDirection");

            migrationBuilder.DropTable(
                name: "WeatherPhenomenon");

            migrationBuilder.DropTable(
                name: "WeatherReport");

            migrationBuilder.DropTable(
                name: "WindDirection");
        }
    }
}
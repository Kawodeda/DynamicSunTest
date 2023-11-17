using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherArchive.Migrations
{
    /// <inheritdoc />
    public partial class AddWeatherReportTimestampUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WeatherReport_Timestamp",
                table: "WeatherReport",
                column: "Timestamp",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WeatherReport_Timestamp",
                table: "WeatherReport");
        }
    }
}

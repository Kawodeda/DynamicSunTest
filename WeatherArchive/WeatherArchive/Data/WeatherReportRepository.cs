using Microsoft.EntityFrameworkCore;
using WeatherArchive.Models;

namespace WeatherArchive.Data
{
    public class WeatherReportRepository : IWeatherReportRepository
    {
        private readonly WeatherArchiveContext _context;

        public WeatherReportRepository(WeatherArchiveContext context)
        {
            _context = context;
        }

        public IEnumerable<WeatherReport> List()
        {
            return _context.WeatherReports
                .Include(report => report.WindDirections)
                .Include(report => report.WeatherPhenomena);
        }

        public async Task<bool> Exists(DateTime timestamp)
        {
            WeatherReport? report = await _context.WeatherReports
                .FirstOrDefaultAsync(report => report.Timestamp == timestamp);

            return report != null;
        }

        public async Task Create(IEnumerable<WeatherReport> weatherReports)
        {
            await _context.WeatherReports.AddRangeAsync(weatherReports);

            await _context.SaveChangesAsync();
        }
    }
}
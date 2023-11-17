using WeatherArchive.Models;

namespace WeatherArchive.Data
{
    public interface IWeatherReportRepository
    {
        IEnumerable<WeatherReport> List();

        Task<bool> Exists(DateTime timestamp);

        Task Create(IEnumerable<WeatherReport> weatherReports);
    }
}
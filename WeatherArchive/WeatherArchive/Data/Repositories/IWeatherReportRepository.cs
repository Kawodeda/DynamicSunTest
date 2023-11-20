using WeatherArchive.Models;

namespace WeatherArchive.Data.Repositories
{
    public interface IWeatherReportRepository
    {
        IEnumerable<WeatherReport> List();

        Task<WeatherReportsSavingResult> SaveIfNotExists(IEnumerable<WeatherReport> weatherReports);
    }
}
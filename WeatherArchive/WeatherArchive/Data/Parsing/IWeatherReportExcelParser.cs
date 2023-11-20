using WeatherArchive.Models;

namespace WeatherArchive.Data.Parsing
{
    public interface IWeatherReportExcelParser
    {
        IEnumerable<WeatherReport> Parse(Stream data);
    }
}
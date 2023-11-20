using WeatherArchive.Data.Parsing;
using WeatherArchive.Data.Repositories;
using WeatherArchive.Models;

namespace WeatherArchive.Services
{
    public class WeatherArchiveUploadService : IWeatherArchiveUploadService
    {
        private readonly IWeatherReportExcelParser _weatherReportParser;
        private readonly IWeatherReportRepository _weatherReportRepository;

        public WeatherArchiveUploadService(IWeatherReportExcelParser weatherReportParser, IWeatherReportRepository weatherReportRepository)
        {
            _weatherReportParser = weatherReportParser;
            _weatherReportRepository = weatherReportRepository;
        }

        public async Task<WeatherArchiveUploadResult> UploadExcel(Stream file)
        {
            var reports = new List<WeatherReport>();
            bool parsingError = false;
            try
            {
                reports.AddRange(_weatherReportParser.Parse(file));
            }
            catch
            {
                parsingError = true;
            }

            WeatherReportsSavingResult result = await _weatherReportRepository.SaveIfNotExists(reports);

            return new WeatherArchiveUploadResult(result.Saved, result.Conflicts, parsingError);
        }
    }
}
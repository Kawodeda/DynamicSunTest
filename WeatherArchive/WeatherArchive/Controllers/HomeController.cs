using Microsoft.AspNetCore.Mvc;
using WeatherArchive.Data.Repositories;
using WeatherArchive.Models;
using WeatherArchive.Models.ViewModels;
using WeatherArchive.Services;

namespace WeatherArchive.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherArchiveUploadService _weatherArchiveUploadService;
        private readonly IWeatherReportRepository _weatherReportRepository;

        public HomeController(IWeatherArchiveUploadService weatherArchiveUploadService, IWeatherReportRepository weatherReportRepository)
        {
            _weatherArchiveUploadService = weatherArchiveUploadService;
            _weatherReportRepository = weatherReportRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewArchive(int page = 0, int? month = null, int? year = null)
        {
            const int pageSize = 60;
            List<WeatherReport> reports = _weatherReportRepository.List()
                .Where(report => month == null || report.Timestamp.Month == month)
                .Where(report => year == null || report.Timestamp.Year == year)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            return View(new ViewArchiveViewModel()
            {
                WeatherReports = reports,
                PageIndex = page,
                NextEnabled = reports.Count == pageSize,
                Month = month,
                Year = year
            });
        }

        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = WeatherArchiveUploadResult.Empty;
            foreach (IFormFile file in files)
            {
                using (Stream stream = file.OpenReadStream())
                {
                    result += await _weatherArchiveUploadService.UploadExcel(stream);
                }
            }

            return View(
                "UploadResult", 
                new FileUploadResultViewModel 
                { 
                    SavedReports = result.SavedReports, 
                    Conflicts = result.Conflicts,
                    ParsingError = result.ParsingError
                });
        }
    }
}
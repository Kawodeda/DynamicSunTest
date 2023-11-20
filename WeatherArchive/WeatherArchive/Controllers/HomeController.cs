using Microsoft.AspNetCore.Mvc;
using WeatherArchive.Models.ViewModels;
using WeatherArchive.Services;

namespace WeatherArchive.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherArchiveUploadService _weatherArchiveUploadService;

        public HomeController(IWeatherArchiveUploadService weatherArchiveUploadService)
        {
            _weatherArchiveUploadService = weatherArchiveUploadService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
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
using System.ComponentModel.DataAnnotations;

namespace WeatherArchive.Models.ViewModels
{
    public class FileUploadViewModel
    {
        [Required(ErrorMessage = "Select at least one file")]
        public required List<IFormFile> Files { get; set; }
    }
}
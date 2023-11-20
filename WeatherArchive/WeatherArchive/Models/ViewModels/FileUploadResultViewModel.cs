namespace WeatherArchive.Models.ViewModels
{
    public class FileUploadResultViewModel
    {
        public int SavedReports { get; set; }

        public int Conflicts { get; set; }

        public bool ParsingError { get; set; }
    }
}
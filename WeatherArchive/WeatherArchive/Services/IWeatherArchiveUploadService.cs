namespace WeatherArchive.Services
{
    public interface IWeatherArchiveUploadService
    {
        Task<WeatherArchiveUploadResult> UploadExcel(Stream file);
    }
}
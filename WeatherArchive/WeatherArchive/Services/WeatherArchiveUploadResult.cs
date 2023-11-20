namespace WeatherArchive.Services
{
    public class WeatherArchiveUploadResult
    {
        public WeatherArchiveUploadResult(int savedReports, int conflicts, bool parsingError)
        {
            SavedReports = savedReports;
            Conflicts = conflicts;
            ParsingError = parsingError;
        }

        public static WeatherArchiveUploadResult Empty
        {
            get
            {
                return new WeatherArchiveUploadResult(0, 0, false);
            }
        }

        public int SavedReports { get; }

        public int Conflicts { get; }

        public bool ParsingError { get; }

        public WeatherArchiveUploadResult Combine(WeatherArchiveUploadResult other)
        {
            return new WeatherArchiveUploadResult(
                SavedReports + other.SavedReports, 
                Conflicts + other.Conflicts, 
                ParsingError || other.ParsingError);
        }

        public static WeatherArchiveUploadResult operator +(WeatherArchiveUploadResult a, WeatherArchiveUploadResult b)
        {
            return a.Combine(b);
        }
    }
}
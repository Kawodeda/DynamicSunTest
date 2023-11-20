namespace WeatherArchive.Data.Repositories
{
    public class WeatherReportsSavingResult
    {
        public WeatherReportsSavingResult(int saved, int conflicts)
        {
            Saved = saved;
            Conflicts = conflicts;
        }

        public int Saved { get; }

        public int Conflicts { get; }
    }
}
namespace WeatherArchive.Models
{
    public class WindDirection
    {
        public WindDirection(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; }

        public string Title { get; }
    }
}
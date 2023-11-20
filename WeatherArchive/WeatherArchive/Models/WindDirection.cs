namespace WeatherArchive.Models
{
    public class WindDirection
    {
        public WindDirection(string title) : this(0, title)
        {

        }

        private WindDirection(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; }

        public string Title { get; }
    }
}
namespace WeatherArchive.Models
{
    public class WeatherPhenomenon
    {
        public WeatherPhenomenon(string title) : this(0, title)
        {

        }

        private WeatherPhenomenon(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; }

        public string Title {  get; }
    }
}
namespace WeatherArchive.Models
{
    public class WeatherPhenomenon
    {
        public WeatherPhenomenon(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; }

        public string Title {  get; }
    }
}
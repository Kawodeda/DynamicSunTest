namespace WeatherArchive.Models
{
    public class WeatherReport
    {
        public WeatherReport(
            int id,
            DateTime timestamp,
            float temperature,
            float humidity,
            float dewPoint,
            float pressure,
            float windSpeed,
            float? cloudiness,
            float cloudBase,
            float? horizontalVisibility)
        {
            Id = id;
            Timestamp = timestamp;
            Temperature = temperature;
            Humidity = humidity;
            DewPoint = dewPoint;
            Pressure = pressure;
            WindSpeed = windSpeed;
            Cloudiness = cloudiness;
            CloudBase = cloudBase;
            HorizontalVisibility = horizontalVisibility;
        }

        public int Id { get; }

        public DateTime Timestamp { get; }

        public float Temperature { get; }

        public float Humidity { get; }

        public float DewPoint { get; }

        public float Pressure { get; }

        public float WindSpeed { get; }

        public float? Cloudiness { get; }

        public float CloudBase { get; }

        public float? HorizontalVisibility { get; }

        public List<WindDirection> WindDirections { get; } = new();

        public List<WeatherPhenomenon> WeatherPhenomena { get; } = new();
    }
}
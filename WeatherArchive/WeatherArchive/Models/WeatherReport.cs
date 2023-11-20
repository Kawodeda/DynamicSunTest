namespace WeatherArchive.Models
{
    public class WeatherReport
    {
        public WeatherReport(
            DateTime timestamp,
            float temperature,
            float humidity,
            float dewPoint,
            float pressure,
            float windSpeed,
            float? cloudiness = null,
            float? cloudBase = null,
            float? horizontalVisibility = null,
            IEnumerable<WindDirection>? windDirections = null,
            IEnumerable<WeatherPhenomenon>? weatherPhenomena = null)
        {
            Id = 0;
            Timestamp = timestamp;
            Temperature = temperature;
            Humidity = humidity;
            DewPoint = dewPoint;
            Pressure = pressure;
            WindSpeed = windSpeed;
            Cloudiness = cloudiness;
            CloudBase = cloudBase;
            HorizontalVisibility = horizontalVisibility;
            
            if (windDirections != null)
            {
                WindDirections.AddRange(windDirections);
            }
            if (weatherPhenomena != null)
            {
                WeatherPhenomena.AddRange(weatherPhenomena);
            }
        }

        private WeatherReport(
            int id,
            DateTime timestamp,
            float temperature,
            float humidity,
            float dewPoint,
            float pressure,
            float windSpeed,
            float? cloudiness = null,
            float? cloudBase = null,
            float? horizontalVisibility = null) 
            : this(
                  timestamp, 
                  temperature, 
                  humidity,
                  dewPoint,
                  pressure,
                  windSpeed,
                  cloudiness,
                  cloudBase,
                  horizontalVisibility)
        {
            Id = id;
        }

        public int Id { get; }

        public DateTime Timestamp { get; }

        public float Temperature { get; }

        public float Humidity { get; }

        public float DewPoint { get; }

        public float Pressure { get; }

        public float WindSpeed { get; }

        public float? Cloudiness { get; }

        public float? CloudBase { get; }

        public float? HorizontalVisibility { get; }

        public List<WindDirection> WindDirections { get; } = new();

        public List<WeatherPhenomenon> WeatherPhenomena { get; } = new();
    }
}
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WeatherArchive.Models;

namespace WeatherArchive.Data.Repositories
{
    public class WeatherReportRepository : IWeatherReportRepository
    {
        private readonly WeatherArchiveContext _context;

        public WeatherReportRepository(WeatherArchiveContext context)
        {
            _context = context;
        }

        public IEnumerable<WeatherReport> List()
        {
            return _context.WeatherReports
                .Include(report => report.WindDirections)
                .Include(report => report.WeatherPhenomena);
        }

        public async Task<WeatherReportsSavingResult> SaveIfNotExists(IEnumerable<WeatherReport> weatherReports)
        {
            return await WithRetryAsync(() => SaveIfNotExistsImpl(weatherReports), 3);
        }

        private async Task<T> WithRetryAsync<T>(Func<Task<T>> taskFactory, int retryCount)
        {
            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    return await taskFactory();
                }
                catch
                {

                }
            }

            throw new Exception();
        }

        private async Task<WeatherReportsSavingResult> SaveIfNotExistsImpl(IEnumerable<WeatherReport> weatherReports)
        {
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            
            List<WeatherReport> weatherReportsToSave = weatherReports
                    .Distinct(new WeatherReportEqualityComparer())
                    .Where(report => !_context.WeatherReports.Any(r => r.Timestamp == report.Timestamp))
                    .ToList();
            int conflictsCount = weatherReports.Count() - weatherReportsToSave.Count();

            await ReplaceWindDirectionsWithExistingEntities(weatherReportsToSave);
            await ReplaceWeatherPhenomenaWithExistingEntities(weatherReportsToSave);

            await _context.WeatherReports.AddRangeAsync(weatherReportsToSave);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new WeatherReportsSavingResult(weatherReportsToSave.Count, conflictsCount);
        }

        private async Task ReplaceWindDirectionsWithExistingEntities(IEnumerable<WeatherReport> weatherReports)
        {
            List<WindDirection> existingWindDirections = await _context.WindDirections.ToListAsync();
            foreach (WeatherReport weatherReport in weatherReports)
            {
                var result = new List<WindDirection>();
                foreach (WindDirection windDirection in weatherReport.WindDirections)
                {
                    WindDirection? existingWindDirection = existingWindDirections.FirstOrDefault(x => x.Title == windDirection.Title);
                    if (existingWindDirection != null)
                    {
                        result.Add(existingWindDirection);
                    }
                    else
                    {
                        result.Add(windDirection);
                        existingWindDirections.Add(windDirection);
                    }
                }
                weatherReport.WindDirections.Clear();
                weatherReport.WindDirections.AddRange(result);
            }
        }

        private async Task ReplaceWeatherPhenomenaWithExistingEntities(IEnumerable<WeatherReport> weatherReports)
        {
            List<WeatherPhenomenon> existingWeatherPhenomena = await _context.WeatherPhenomena.ToListAsync();
            foreach (WeatherReport weatherReport in weatherReports)
            {
                var result = new List<WeatherPhenomenon>();
                foreach (WeatherPhenomenon weatherPhenomenon in weatherReport.WeatherPhenomena)
                {
                    WeatherPhenomenon? existingWeatherPhenomenon = existingWeatherPhenomena.FirstOrDefault(x => x.Title == weatherPhenomenon.Title);
                    if (existingWeatherPhenomenon != null)
                    {
                        result.Add(existingWeatherPhenomenon);
                    }
                    else
                    {
                        result.Add(weatherPhenomenon);
                        existingWeatherPhenomena.Add(weatherPhenomenon);
                    }
                }
                weatherReport.WeatherPhenomena.Clear();
                weatherReport.WeatherPhenomena.AddRange(result);
            }
        }
    }

    class WeatherReportEqualityComparer : IEqualityComparer<WeatherReport>
    {
        public bool Equals(WeatherReport? x, WeatherReport? y)
        {
            return x?.Timestamp == y?.Timestamp;
        }

        public int GetHashCode([DisallowNull] WeatherReport obj)
        {
            return obj.Timestamp.GetHashCode();
        }
    }
}
﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WeatherArchive.Models;

namespace WeatherArchive.Data.Repositories
{
    public class WeatherReportRepository : IWeatherReportRepository
    {
        private readonly WeatherArchiveContext _context;
        private static readonly SemaphoreSlim _dbAccessSemaphore = new(1, 1);

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
            await _dbAccessSemaphore.WaitAsync();
            try
            {
                List<WeatherReport> weatherReportsToSave = weatherReports
                        .Distinct(new WeatherReportEqualityComparer())
                        .Where(report => !_context.WeatherReports.Any(r => r.Timestamp == report.Timestamp))
                        .ToList();
                int conflictsCount = weatherReports.Count() - weatherReportsToSave.Count();

                await ReplaceWindDirectionsWithExistingEntities(weatherReportsToSave);
                await ReplaceWeatherPhenomenaWithExistingEntities(weatherReportsToSave);

                await _context.WeatherReports.AddRangeAsync(weatherReportsToSave);
                await _context.SaveChangesAsync();

                return new WeatherReportsSavingResult(weatherReportsToSave.Count, conflictsCount);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occured while saving weather reports", ex);
            }
            finally
            {
                _dbAccessSemaphore.Release();
            }
        }

        private async Task ReplaceWindDirectionsWithExistingEntities(IEnumerable<WeatherReport> weatherReports)
        {
            // Предполагается, что в таблице WindDirection будет не много данных,
            // поэтому можем загрузить все в память
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
            // Предполагается, что в таблице WeatherPhenomenon будет не много данных,
            // поэтому можем загрузить все в память
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
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using WeatherArchive.Models;

namespace WeatherArchive.Data.Parsing
{
    public class WeatherReportExcelParser : IWeatherReportExcelParser
    {
        private const int _startRow = 4;

        public IEnumerable<WeatherReport> Parse(Stream data)
        {
            var workbook = new XSSFWorkbook(data);

            return GetSheets(workbook)
                .SelectMany(ParseSheet);
        }

        private IEnumerable<WeatherReport> ParseSheet(ISheet sheet)
        {
            return GetRows(sheet, _startRow)
                .Select(ParseRow);
        }

        private WeatherReport ParseRow(IRow row)
        {
            if (!DateOnly.TryParse(row.GetCell(0).StringCellValue, out DateOnly date))
            {
                throw new ArgumentException();
            }
            if (!TimeOnly.TryParse(row.GetCell(1).StringCellValue, out TimeOnly time))
            {
                throw new ArgumentException();
            }

            DateTime timestamp = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, time.Millisecond, time.Microsecond);
            float temperature = Convert.ToSingle(row.GetCell(2).NumericCellValue);
            float humidity = Convert.ToSingle(row.GetCell(3).NumericCellValue);
            float dewPoint = Convert.ToSingle(row.GetCell(4).NumericCellValue);
            float pressure = Convert.ToSingle(row.GetCell(5).NumericCellValue);
            float windSpeed = ParseNumericCell(row.GetCell(7), 0);
            float? cloudiness = ParseNullableNumericCell(row.GetCell(8));
            float cloudBase = ParseNumericCell(row.GetCell(9), 0);
            float? horizontalVisibility = ParseNullableNumericCell(row.GetCell(10));

            var windDirections = row.GetCell(6).StringCellValue
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(title => new WindDirection(title));

            var weatherPhenomena = row.GetCell(11)?.StringCellValue
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(title => new WeatherPhenomenon(title)) 
                    ?? new List<WeatherPhenomenon>();

            return new WeatherReport(
                timestamp,
                temperature,
                humidity,
                dewPoint,
                pressure,
                windSpeed,
                cloudiness,
                cloudBase,
                horizontalVisibility,
                windDirections,
                weatherPhenomena);
        }

        private float ParseNumericCell(ICell cell, float defaultValue)
        {
            return cell.CellType == CellType.Numeric
                ? Convert.ToSingle(cell.NumericCellValue)
                : defaultValue;
        }

        private float? ParseNullableNumericCell(ICell cell)
        {
            return cell.CellType == CellType.Numeric
                ? Convert.ToSingle(cell.NumericCellValue)
                : null;
        }

        private IEnumerable<ISheet> GetSheets(IWorkbook workbook)
        {
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                yield return workbook.GetSheetAt(i);
            }
        }

        private IEnumerable<IRow> GetRows(ISheet sheet, int startAt)
        {
            for (int i = startAt; i <= sheet.LastRowNum; i++)
            {
                yield return sheet.GetRow(i);
            }
        }
    }
}
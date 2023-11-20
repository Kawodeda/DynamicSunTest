using Microsoft.AspNetCore.Mvc.Rendering;

namespace WeatherArchive.Models.ViewModels
{
    public class ViewArchiveViewModel
    {
        public required List<WeatherReport> WeatherReports { get; set; }

        public int PageIndex { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }

        public bool NextEnabled { get; set; }

        public List<SelectListItem> Months { get; } = new List<SelectListItem>() {
            new SelectListItem { Text = "--- none ---", Value = "" },
            new SelectListItem { Text = "January", Value = "1" },
            new SelectListItem { Text = "February", Value = "2" },
            new SelectListItem { Text = "March", Value = "3" },
            new SelectListItem { Text = "April", Value = "4" },
            new SelectListItem { Text = "May", Value = "5" },
            new SelectListItem { Text = "June", Value = "6" },
            new SelectListItem { Text = "July", Value = "7" },
            new SelectListItem { Text = "August", Value = "8" },
            new SelectListItem { Text = "September", Value = "9" },
            new SelectListItem { Text = "October", Value = "10" },
            new SelectListItem { Text = "November", Value = "11" },
            new SelectListItem { Text = "December", Value = "12" }
        };

        public bool PreviousEnabled
        {
            get
            {
                return PageIndex > 0;
            }
        }
    }
}
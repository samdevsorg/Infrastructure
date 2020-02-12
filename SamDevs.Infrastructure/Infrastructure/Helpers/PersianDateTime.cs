using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Infrastructure.Helpers
{
    public class PersianDateTime
    {
        private readonly PersianCalendar _persianCalendar = new PersianCalendar();
        private DateTime _originalDate = DateTime.Now;
        private string _persianDate;
        private int _month;

        public PersianDateTime(DateTime dateTime)
        {
            OriginalDate = dateTime;
        }
        public PersianDateTime(string persianDateTime)
        {
            PersianDate = persianDateTime;
        }
        public DateTime OriginalDate
        {
            get => _originalDate;
            set
            {
                _originalDate = value;
                Day = _persianCalendar.GetDayOfMonth(value);
                Month = _persianCalendar.GetMonth(value);
                Year = _persianCalendar.GetYear(value);
                _persianDate = $"{Year:0000}/{Month:00}/{Day:00}";
                var dayInt = (int)_persianCalendar.GetDayOfWeek(value);
                DayOfWeek = dayInt == 6 ? PersianWeekDay.Shanbe : (PersianWeekDay)(dayInt + 2);
            }
        }
        public string PersianDate
        {
            get => _persianDate;
            set
            {
                if (!string.IsNullOrEmpty(value) && Regex.IsMatch(value,@"^(13|14|15)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"))
                {
                    var values = value.Split('/');
                    if (values.Length != 3) return;
                    if (!int.TryParse(values[0], out var year) || 1300 > year || year > 1500) return;
                    if (!int.TryParse(values[1], out var month) || 1 > month || month > 12) return;
                    if (!int.TryParse(values[2], out var day) || 1 > day || day > 31) return;
                    if (month > 6 && day >= 31) return;
                    if (!_persianCalendar.IsLeapYear(year) && month == 12 && day == 30) return;

                    Year = year;
                    Month = month;
                    Day = day;
                    _persianDate = value;
                    _originalDate = _persianCalendar.ToDateTime(Year, Month, Day, 0, 0, 0, 0);
                }
            }
        }

        [Range(1, 31)]
        public int Day { get; set; }
        public PersianWeekDay DayOfWeek { get; set; }

        [Range(1, 12)]
        public int Month
        {
            get => _month;
            set
            {
                _month = value;
                MonthName = (PersianMonth)value;
            }
        }
        public PersianMonth MonthName { get; set; }

        [Range(1300, 1500)]
        public int Year { get; set; }
    }

    public enum PersianMonth
    {
        Farvardin = 1,
        Ordibehesht = 2,
        Khordad = 3,
        Tir = 4,
        Mordad = 5,
        Shahrivar = 6,
        Mehr = 7,
        Aban = 8,
        Azar = 9,
        Dey = 10,
        Bahman = 11,
        Esfand = 12
    }

    public enum PersianWeekDay
    {
        Shanbe = 1,
        YekShanbe = 2,
        DoShanbe = 3,
        SeShanbe = 4,
        ChaharShanbe = 5,
        PanjShanbe = 6,
        Jome = 7
    }
}

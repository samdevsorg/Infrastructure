using System;

namespace SamDevs.Infrastructure.Helpers {
    public class DateTimePlus {
        private DateTime _date;

        #region Constructors
        public DateTimePlus() {
            _date = new DateTime();
        }
        public DateTimePlus(int year, int month = 1, int day = 1, int hour = 0, int minute = 0, int second = 0, int millisecond = 0) {
            _date = new DateTime(year, month, day, hour, minute, second, millisecond);
        }
        public DateTimePlus(long ticks) {
            _date = new DateTime(ticks);
        }

        public DateTimePlus(DateTime date)
            : this(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond) {
        }
        #endregion Constructors

        #region Static Properties
        public static DateTimePlus Now => new DateTimePlus(DateTime.Now);
        public static DateTimePlus UtcNow => new DateTimePlus(DateTime.UtcNow);
        public static DateTimePlus Today => new DateTimePlus(DateTime.Today);
        public static DateTimePlus Yesterday => Today.AddDays(-1);
        public static DateTimePlus Tomorrow => new DateTimePlus(DateTime.Today.AddDays(1));
        #endregion Static Properties

        #region Properties
        public int Year => _date.Year;
        public int Month => _date.Month;
        public int Day => _date.Day;
        public int Hour => _date.Hour;
        public int Minute => _date.Minute;
        public int Second => _date.Second;
        public int Millisecond => _date.Millisecond;

        public DateTimePlus FirstDateOfYear => new DateTimePlus(_date.Year);
        public DateTimePlus LastDateOfYear
            => new DateTimePlus(_date.Year, 12, 31, 23, 59, 59, 999);
        public DateTimePlus FirstDateOfMonth => new DateTimePlus(_date.Year, _date.Month);
        public DateTimePlus LastDateOfMonth
            => new DateTimePlus(_date.Year, _date.Month, DaysInMonth(_date.Year, _date.Month), 23, 59, 59, 999);
        public DateTimePlus FirstDateOfWeek 
            => Today.AddDays(-(DayOfWeek == DayOfWeek.Saturday ? 0 : (int)DayOfWeek + 1));
        public DateTimePlus LastDateOfWeek
            => FirstDateOfWeek.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

        public DayOfWeek DayOfWeek => _date.DayOfWeek;
        #endregion Properties


        #region Static Methods
        public static int DaysInMonth(int year, int month) => DateTime.DaysInMonth(year, month);
        #endregion Static Methods

        #region Public Methods
        public DateTimePlus AddYears(int value) => new DateTimePlus(_date.AddYears(value));
        public DateTimePlus AddMonths(int value) => new DateTimePlus(_date.AddMonths(value));
        public DateTimePlus AddDays(double value) => new DateTimePlus(_date.AddDays(value));
        public DateTimePlus AddHours(double value) => new DateTimePlus(_date.AddHours(value));
        public DateTimePlus AddMinutes(double value) => new DateTimePlus(_date.AddMinutes(value));
        public DateTimePlus AddSeconds(double value) => new DateTimePlus(_date.AddSeconds(value));
        public DateTimePlus AddMilliseconds(double value) => new DateTimePlus(_date.AddMilliseconds(value));
        public DateTime ToDateTime() => _date;
        public override string ToString() => _date.ToString();
        #endregion Public Methods



    }
}
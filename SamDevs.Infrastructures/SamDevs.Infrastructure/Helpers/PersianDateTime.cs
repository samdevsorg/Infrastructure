using System;
using System.Globalization;

namespace SamDevs.Infrastructure.Helpers
{
    //public enum PersianDayOfWeek
    //{

    //}
    public class PersianDateTime
    {

        /// <summary>
        /// Represents the largest possible value of SamDevs.PersianDateTime. This field is read-only.
        /// </summary>
        public static readonly PersianDateTime MaxValue = new PersianDateTime(9378, 10, 13, 23, 59, 59, 999);

        /// <summary>
        /// Represents the smallest possible value of SamDevs.DateTimeLibrary. This field is read-only.
        /// </summary>
        public static readonly PersianDateTime MinValue = new PersianDateTime(1, 1, 1, 12, 0, 0, 0);

        /// <summary>
        /// Gets the date component of this instance.
        /// 
        /// Returns:
        ///     A new SamDevs.DateTimeLibrary with the same date as this instance, and the time value
        ///     set to 12:00:00 midnight (00:00:00).
        /// </summary>
        public PersianDateTime Date => new PersianDateTime(Year, Month, Day, 0, 0, 0, 0);

        /// <summary>
        ///Gets the day of the week represented by this instance.
        ///
        /// Returns:
        ///     A System.DayOfWeek enumerated constant that indicates the day of the week.
        ///     This property value ranges from zero, indicating Sunday, to six, indicating
        ///     Saturday.
        /// </summary>
        public int DayOfWeek
        {
            get
            {
                var dow = ((int)ToDateTime().DayOfWeek + 2) % 8;
                if (dow > 0) return dow;
                return 1;
            }
        }

        public string DayName
        {
            get
            {
                switch (DayOfWeek)
                {
                    case 1:
                        return "شنبه";
                    case 2:
                        return "يكشنبه";
                    case 3:
                        return "دوشنبه";
                    case 4:
                        return "سه شنبه";
                    case 5:
                        return "چهارشنبه";
                    case 6:
                        return "پنجشنبه";
                    default:
                        return "جمعه";
                }
            }
        }
        public string ShortDayName
        {
            get
            {
                switch (DayOfWeek)
                {
                    case 1:
                        return "ش";
                    case 2:
                        return "ي";
                    case 3:
                        return "د";
                    case 4:
                        return "س";
                    case 5:
                        return "چ";
                    case 6:
                        return "پ";
                    default:
                        return "ج";
                }
            }
        }

        /// <summary>
        /// Gets the day of the year represented by this instance.
        /// 
        /// Returns:
        ///      The day of the year, expressed as a value between 1 and 366.
        /// </summary>
        public int DayOfYear
        {
            get
            {

                var sum = 0;
                for (var i = 1; i < Month; i++)
                    sum += DaysInMonth(Year, i);

                return sum + Day;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public static PersianDateTime Now => new PersianDateTime(DateTime.Now);


        /// <summary>
        /// 
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        public static PersianDateTime Today => new PersianDateTime(DateTime.Today);

        //public static PersianDateTime UtcNow { get; private set; }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="dayOfWeek"></param>
        ///// <returns></returns>
        //private string GetDayOfWeek(DayOfWeek dayOfWeek)
        //{
        //    string dow = "";
        //    switch (dayOfWeek)
        //    {
        //        case System.DayOfWeek.Saturday:
        //            dow = "شنبه";
        //            break;
        //        case System.DayOfWeek.Sunday:
        //            dow = "يكشنبه";
        //            break;
        //        case System.DayOfWeek.Monday:
        //            dow = "دوشنبه";
        //            break;
        //        case System.DayOfWeek.Tuesday:
        //            dow = "سه شنبه";
        //            break;
        //        case System.DayOfWeek.Wednesday:
        //            dow = "چهارشنبه";
        //            break;
        //        case System.DayOfWeek.Thursday:
        //            dow = "پنجشنبه";
        //            break;
        //        case System.DayOfWeek.Friday:
        //            dow = "جمعه";
        //            break;
        //    }
        //    return dow;
        //}

        public static string GetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "فروردين";
                case 2:
                    return "ارديبهشت";
                case 3:
                    return "خرداد";
                case 4:
                    return "تير";
                case 5:
                    return "مرداد";
                case 6:
                    return "شهريور";
                case 7:
                    return "مهر";
                case 8:
                    return "آبان";
                case 9:
                    return "آذر";
                case 10:
                    return "دي";
                case 11:
                    return "بهمن";
                case 12:
                    return "اسفند";
                default:
                    return "";
            }
        }
        public static string GetShortMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "فروردين";
                case 2:
                    return "ارديبهشت";
                case 3:
                    return "خرداد";
                case 4:
                    return "تير";
                case 5:
                    return "مرداد";
                case 6:
                    return "شهريور";
                case 7:
                    return "مهر";
                case 8:
                    return "آبان";
                case 9:
                    return "آذر";
                case 10:
                    return "دي";
                case 11:
                    return "بهمن";
                case 12:
                    return "اسفند";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public PersianDateTime() : this(1, 1, 1, 12, 0, 0) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticks"></param>
        public PersianDateTime(long ticks) : this(new DateTime(ticks)) { }

        //public PersianDateTime(long ticks,DateTimeKind kind)
        //{

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public PersianDateTime(int year, int month, int day) : this(year, month, day, 12, 0, 0, 0) { }

        //public PersianDateTime(int year, int month, int day, Calendar calendar)
        //{

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        public PersianDateTime(int year, int month, int day, int hour, int minute, int second)
            : this(year, month, day, hour, minute, second, 0) { }

        //public PersianDateTime(int year, int month, int day, int hour, int minute, int second, Calendar calendar)
        //{

        //}
        //public PersianDateTime(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
        //{

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        public PersianDateTime(DateTime date)
        {
            var cc = new PersianCalendar();
            if (date >= DateTime.Parse("0622/3/21"))
            {
                Year = cc.GetYear(date);
                Month = cc.GetMonth(date);
                Day = cc.GetDayOfMonth(date);
                Hour = cc.GetHour(date);
                Minute = cc.GetMinute(date);
                Second = cc.GetSecond(date);
                Millisecond = (int)cc.GetMilliseconds(date);
            }
            else
            {
                Year = 1;
                Month = 1;
                Day = 1;
                Hour = 0;
                Minute = 0;
                Second = 0;
                Millisecond = 0;

            }
        }

        public static PersianDateTime FromDateTime(DateTime date)
        {
            return new PersianDateTime(date);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <param name="millisecond"></param>
        public PersianDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
        {
            Year = year;
            Month = month;
            Day = day;
            Hour = hour;
            Minute = minute;
            Second = second;
            Millisecond = millisecond;
        }

        //public PersianDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar)
        //{

        //}
        //public PersianDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, DateTimeKind kind)
        //{

        //}
        //public PersianDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, DateTimeKind kind)
        //{

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static TimeSpan operator -(PersianDateTime d1, PersianDateTime d2)
        {
            return ToDateTime(d1) - ToDateTime(d2);
        }

        //public static PersianDateTime operator -(PersianDateTime d, TimeSpan t)
        //{
        //    DateTime date = PersianDateTime.ToDateTime(d);


        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool operator !=(PersianDateTime d1, PersianDateTime d2)
        {
            return ToDateTime(d1) != ToDateTime(d2);
        }

        //public static DateTime operator +(PersianDateTime d, TimeSpan t)
        //{
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool operator <(PersianDateTime d1, PersianDateTime d2)
        {
            return ToDateTime(d1) < ToDateTime(d2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool operator <=(PersianDateTime d1, PersianDateTime d2)
        {
            return ToDateTime(d1) <= ToDateTime(d2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool operator ==(PersianDateTime d1, PersianDateTime d2)
        {
            return ToDateTime(d1) == ToDateTime(d2);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool operator >(PersianDateTime d1, PersianDateTime d2)
        {
            return ToDateTime(d1) > ToDateTime(d2);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool operator >=(PersianDateTime d1, PersianDateTime d2)
        {
            return ToDateTime(d1) >= ToDateTime(d2);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(PersianDateTime date)
        {
            var cc = new PersianCalendar();
            return cc.ToDateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
        }

        public static DateTime ToDateTime(string date)
        {
            return ToDateTime(Parse(date));
        }

        public long Ticks => ToDateTime().Ticks;

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan TimeOfDay => ToDateTime().TimeOfDay;


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime ToDateTime()
        {
            return ToDateTime(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PersianDateTime Add(TimeSpan value)
        {
            return new PersianDateTime(ToDateTime(this).Add(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PersianDateTime AddDays(double value)
        {
            return new PersianDateTime(ToDateTime(this).AddDays(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PersianDateTime AddHours(double value)
        {
            return new PersianDateTime(ToDateTime(this).AddHours(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PersianDateTime AddMilliseconds(double value)
        {
            return new PersianDateTime(ToDateTime(this).AddMilliseconds(value));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PersianDateTime AddMinutes(double value)
        {
            return new PersianDateTime(ToDateTime(this).AddMinutes(value));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="months"></param>
        /// <returns></returns>
        public PersianDateTime AddMonths(int value)
        {
            return new PersianDateTime(ToDateTime(this).AddMonths(value));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PersianDateTime AddSeconds(double value)
        {
            return new PersianDateTime(ToDateTime(this).AddSeconds(value));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PersianDateTime AddTicks(long value)
        {
            return new PersianDateTime(ToDateTime(this).AddTicks(value));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PersianDateTime AddYears(int value)
        {
            return new PersianDateTime(ToDateTime(this).AddYears(value));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static int Compare(PersianDateTime d1, PersianDateTime d2)
        {

            return DateTime.Compare(ToDateTime(d1), ToDateTime(d2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int CompareTo(PersianDateTime value)
        {
            return ToDateTime().CompareTo(ToDateTime(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int CompareTo(object value)
        {
            return ToDateTime().CompareTo(ToDateTime(value.ToString()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int DaysInMonth(int year, int month)
        {
            if (month < 7)
                return 31;
            if (month == 12 && !IsLeapYear(year))
                return 29;
            return 30;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Equals(PersianDateTime value)
        {
            return ToDateTime().Equals(ToDateTime(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool Equals(object value)
        {
            return ToDateTime().Equals(value);
        }

        public static bool Equals(PersianDateTime d1, PersianDateTime d2)
        {
            return DateTime.Equals(ToDateTime(d1), ToDateTime(d2));
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public static bool IsLeapYear(int year)
        {
            var pc = new PersianCalendar();
            return pc.IsLeapYear(year);
        }

        public static PersianDateTime Parse(string s)
        {
            PersianDateTime pdate = null;
            try
            {
                var d = s.Split('/');
                if (d.Length != 3)
                    throw new FormatException("String was not recognized as a valid PersianDateTime.");
                int year = Convert.ToInt32(d[0]), month = Convert.ToInt32(d[1]), day = Convert.ToInt32(d[2].Split(' ')[0]);
                //int hour = 12, minute = 0, second = 0, millisecond = 0;
                //d = s.Split(' ');
                //if (d.Length > 2) {
                //    d = d[1]
                //}
                var cc = new PersianCalendar();

                pdate = new PersianDateTime(cc.ToDateTime(year, month, day, 12, 0, 0, 0));
            }
            catch
            {
                throw new Exception("تاريخ نا معتبر مي باشد.");
            }
            return pdate;
        }


        public TimeSpan Subtract(PersianDateTime value)
        {
            return ToDateTime().Subtract(ToDateTime(value));
        }




        public string ToString(string format)
        {
            var formattedText = "";

            if (format != null)
            {
                for (int i = 0, len = format.Length; i < len; i++)
                {
                    var wildcard = format[i];
                    var part = wildcard.ToString();
                    i++;
                    while (i < len && format[i] == wildcard)
                    {
                        part += format[i];
                        i++;
                    }
                    i--;
                    switch (part)
                    {

                        case "y":
                        case "yy":
                        case "yyy":
                        case "yyyy":
                            formattedText += Year.ToString().PadLeft(part.Length, '0');
                            break;
                        case "MMMM":
                            formattedText += GetMonthName(Month);
                            break;
                        case "MMM":
                            formattedText += GetShortMonthName(Month);
                            break;
                        case "MM":
                        case "M":
                            formattedText += Month.ToString().PadLeft(part.Length, '0');
                            break;
                        case "dddd":
                            formattedText += DayName;
                            break;
                        case "ddd":
                            formattedText += ShortDayName;
                            break;
                        case "dd":
                        case "d":
                            formattedText += Day.ToString().PadLeft(part.Length, '0');
                            break;
                        case "HH":
                        case "H":
                            formattedText += Hour.ToString().PadLeft(part.Length, '0');
                            break;
                        case "hh":
                        case "h":
                            formattedText += (Hour % 12 == 0 ? 12 : Hour % 12).ToString().PadLeft(part.Length, '0');
                            break;
                        case "mm":
                        case "m":
                            formattedText += Minute.ToString().PadLeft(part.Length, '0');
                            break;
                        case "ss":
                        case "s":
                            formattedText += Second.ToString().PadLeft(part.Length, '0');
                            break;
                        case "fffffff":
                        case "ffffff":
                        case "fffff":
                        case "ffff":
                        case "fff":
                        case "ff":
                        case "f":
                            formattedText += Millisecond.ToString().PadLeft(part.Length, '0');
                            break;
                        case "tt":
                        case "t":
                            formattedText += (Hour < 12 ? "صبح" : "عصر").Substring(0, part.Length == 1 ? 1 : 3);
                            break;
                        default:
                            formattedText += part;
                            break;
                    }
                }
            }

            return formattedText;
        }

        public static bool TryParse(string s, out PersianDateTime result)
        {
            try
            {
                string[] d = s.Split('/');
                if (d.Length != 3)
                {
                    result = null;
                    return false;
                }
                int year = Convert.ToInt32(d[0]), month = Convert.ToInt32(d[1]), day = Convert.ToInt32(d[2]);
                PersianCalendar cc = new PersianCalendar();
                result = new PersianDateTime(cc.ToDateTime(year, month, day, 12, 0, 0, 0));
            }
            catch
            {
                result = null;
                return false;
            }
            return true;
        }


        public override string ToString()
        {
            return Year + "/" + Month.ToString().PadLeft(2, '0') + "/" + Day.ToString().PadLeft(2, '0') +
                   " " + Hour.ToString().PadLeft(2, '0') + ":" + Minute.ToString().PadLeft(2, '0') + ":" + Second.ToString().PadLeft(2, '0') + "." + Millisecond.ToString().PadLeft(3, '0');
        }

        /// <summary>
        /// Gets the day of the month represented by this instance.
        /// 
        /// Returns:
        ///      The day component, expressed as a value between 1 and 31.
        /// </summary>
        public int Day { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public int Hour { get; protected set; }

        //public DateTimeKind Kind { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public int Millisecond { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public int Minute { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public int Month { get; protected set; }


        /// <summary>
        /// 
        /// </summary>
        public int Year { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public int Second { get; protected set; }

        public string ToLongDateString()
        {
            return Year + "/" + Month.ToString().PadLeft(2, '0') + "/" + Day.ToString().PadLeft(2, '0');
        }

        public string ToLongTimeString()
        {
            return Hour.ToString().PadLeft(2, '0') + ":" + Minute.ToString().PadLeft(2, '0') + ":" + Second.ToString().PadLeft(2, '0');
        }

        //public double ToOADate(){}

        public string ToShortDateString()
        {
            return Year.ToString().Substring(2, 2) + "/" + Month.ToString().PadLeft(2, '0') + "/" + Day.ToString().PadLeft(2, '0');
        }

        public string ToShortTimeString()
        {
            return Hour.ToString().PadLeft(2, '0') + ":" + Minute.ToString().PadLeft(2, '0');
        }


    }
}

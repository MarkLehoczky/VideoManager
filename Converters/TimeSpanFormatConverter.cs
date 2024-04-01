using System;
using System.Globalization;
using System.Windows.Data;

namespace VideoManager.Converters
{
    [ValueConversion(typeof(TimeSpan), typeof(string))]
    public class TimeSpanFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan time)
                if ((int)time.TotalHours > 0)
                    return $"{time.TotalHours,0:##0}:{time.Minutes,0:00}:{time.Seconds,0:00}";
                else
                    return $"{time.Minutes,0:#0}:{time.Seconds,0:00}";

            else
                throw new ArgumentException($"Parameter is not correct '{nameof(TimeSpan)}' type", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
                return TimeSpan.Parse(str);

            else
                throw new ArgumentException($"Parameter is not correct {nameof(String)} type", nameof(value));
        }
    }
}

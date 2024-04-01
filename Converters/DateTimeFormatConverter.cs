using System;
using System.Globalization;
using System.Windows.Data;

namespace VideoManager.Converters
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateTimeFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime date)
                return $"{date:yyyy. MMM. dd.}";

            else
                throw new ArgumentException($"Parameter is not correct '{nameof(DateTime)}' type", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
                return DateTime.Parse(str);

            else
                throw new ArgumentException($"Parameter is not correct {nameof(String)} type", nameof(value));
        }
    }
}

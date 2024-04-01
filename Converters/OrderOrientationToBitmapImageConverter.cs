using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using VideoManager.Processes;

namespace VideoManager.Converters
{
    [ValueConversion(typeof(OrderPreference.OrderOrientation), typeof(BitmapImage))]

    public class OrderOrientationToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is OrderPreference.OrderOrientation orientation)
                if (orientation == OrderPreference.OrderOrientation.ASCENDING)
                    return Extensions.EmbeddedImage(@"Resources\order_asc_icon.png");
                else if (orientation == OrderPreference.OrderOrientation.DESCENDING)
                    return Extensions.EmbeddedImage(@"Resources\order_desc_icon.png");
                else
                    throw new ArgumentException($"Parameter is not correct value", nameof(value));

            else
                throw new ArgumentException($"Parameter is not correct '{nameof(OrderPreference.OrderOrientation)}' type", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BitmapImage img)
                if (Path.GetFullPath(img.BaseUri.AbsolutePath) == Path.GetFullPath(@"Resources\order_asc_icon.png"))
                    return OrderPreference.OrderOrientation.ASCENDING;
                else if (Path.GetFullPath(img.BaseUri.AbsolutePath) == Path.GetFullPath(@"Resources\order_desc_icon.png"))
                    return OrderPreference.OrderOrientation.DESCENDING;
                else
                    throw new ArgumentException($"Parameter is not correct value", nameof(value));

            else
                throw new ArgumentException($"Parameter is not correct '{nameof(BitmapImage)}' type", nameof(value));
        }
    }
}

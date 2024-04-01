using System.Windows;
using System.Windows.Media;

namespace VideoManager.Processes
{
    public class Notification
    {
        public static Notification Singleton;

        public string Message { get; set; }
        public SolidColorBrush Color { get; set; }
        public Visibility Visibility { get; set; }

        static Notification()
        {
            Singleton = new Notification();
        }
        protected Notification()
        {
            Message = string.Empty;
            Color = Brushes.Black;
            Visibility = Visibility.Hidden;
        }
    }
}

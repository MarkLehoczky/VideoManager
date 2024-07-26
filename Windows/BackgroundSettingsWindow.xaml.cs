using System.Windows;
using System.Windows.Media;

namespace VideoManager.Windows
{
    /// <summary>
    /// Interaction logic for BackgroundSettingsWindow.xaml
    /// </summary>
    public partial class BackgroundSettingsWindow : Window
    {
        public BackgroundSettingsWindow()
        {
            InitializeComponent();

            bg_color.Fill = (Brush)Application.Current.Resources["BackgroundColor"];
            slider_red.Value = ((SolidColorBrush)bg_color.Fill).Color.R;
            slider_green.Value = ((SolidColorBrush)bg_color.Fill).Color.G;
            slider_blue.Value = ((SolidColorBrush)bg_color.Fill).Color.B;
        }

        public void UpdateColorSample(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (bg_color != null)
                bg_color.Fill = new SolidColorBrush(Color.FromRgb((byte)slider_red.Value, (byte)slider_green.Value, (byte)slider_blue.Value));
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["BackgroundColor"] = bg_color.Fill;
            Close();
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

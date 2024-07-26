using System.Windows;

namespace VideoManager.Windows
{
    /// <summary>
    /// Interaction logic for TitleSettingWindow.xaml
    /// </summary>
    public partial class TitleSettingsWindow : Window
    {
        public TitleSettingsWindow()
        {
            InitializeComponent();

            font_family.ItemsSource = Extensions.FontFamilies;
            font_weight.ItemsSource = Extensions.FontWeights;
            font_color.ItemsSource = Extensions.FontColors;
            font_wrap.ItemsSource = Extensions.TextWrappings;
            font_trim.ItemsSource = Extensions.TextTrimmings;

            font_size.Text = Application.Current.Resources["TitleFontSize"].ToString();
            font_family.SelectedItem = (System.Windows.Media.FontFamily)Application.Current.Resources["TitleFontFamily"];
            font_weight.SelectedItem = (FontWeight)Application.Current.Resources["TitleFontWeight"];
            font_color.SelectedItem = (System.Windows.Media.Brush)Application.Current.Resources["TitleForeground"];
            font_wrap.SelectedItem = (TextWrapping)Application.Current.Resources["TitleTextWrapping"];
            font_trim.SelectedItem = (TextTrimming)Application.Current.Resources["TitleTextTrimming"];
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(font_size.Text, out double size) && size >= 0)
                Application.Current.Resources["TitleFontSize"] = size;

            Application.Current.Resources["TitleFontFamily"] = font_family.SelectedItem;
            Application.Current.Resources["TitleFontWeight"] = font_weight.SelectedItem;
            Application.Current.Resources["TitleForeground"] = font_color.SelectedItem;
            Application.Current.Resources["TitleTextWrapping"] = font_wrap.SelectedItem;
            Application.Current.Resources["TitleTextTrimming"] = font_trim.SelectedItem;

            Close();
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

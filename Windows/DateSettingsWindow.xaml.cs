using System.Windows;

namespace VideoManager.Windows
{
    /// <summary>
    /// Interaction logic for DateSettingWindow.xaml
    /// </summary>
    public partial class DateSettingsWindow : Window
    {
        public DateSettingsWindow()
        {
            InitializeComponent();

            font_family.ItemsSource = Extensions.FontFamilies;
            font_weight.ItemsSource = Extensions.FontWeights;
            font_color.ItemsSource = Extensions.FontColors;
            font_align.ItemsSource = Extensions.HorizontalAlignments;

            font_size.Text = Application.Current.Resources["DateFontSize"].ToString();
            font_family.SelectedItem = (System.Windows.Media.FontFamily)Application.Current.Resources["DateFontFamily"];
            font_weight.SelectedItem = (FontWeight)Application.Current.Resources["DateFontWeight"];
            font_color.SelectedItem = (System.Windows.Media.Brush)Application.Current.Resources["DateForeground"];
            font_align.SelectedItem = (HorizontalAlignment)Application.Current.Resources["DateAlignment"];
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(font_size.Text, out double size) && size >= 0)
                Application.Current.Resources["DateFontSize"] = size;

            Application.Current.Resources["DateFontFamily"] = font_family.SelectedItem;
            Application.Current.Resources["DateFontWeight"] = font_weight.SelectedItem;
            Application.Current.Resources["DateForeground"] = font_color.SelectedItem;
            Application.Current.Resources["DateAlignment"] = font_align.SelectedItem;

            Close();
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

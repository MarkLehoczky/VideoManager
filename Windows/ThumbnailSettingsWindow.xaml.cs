using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VideoManager.Windows
{
    /// <summary>
    /// Interaction logic for ThumbnailSettingWindow.xaml
    /// </summary>
    public partial class ThumbnailSettingsWindow : Window
    {
        public ThumbnailSettingsWindow()
        {
            InitializeComponent();

            thumbnail_height.Text = Application.Current.Resources["ThumbnailHeight"].ToString();
            thumbnail_width.Text = Application.Current.Resources["ThumbnailWidth"].ToString();

        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(thumbnail_height.Text, out double height) && height > 0)
                Application.Current.Resources["ThumbnailHeight"] = height;
            if (double.TryParse(thumbnail_width.Text, out double width) && width > 0)
                Application.Current.Resources["ThumbnailWidth"] = width;

            Close();
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

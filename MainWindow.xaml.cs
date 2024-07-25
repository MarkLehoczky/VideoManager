using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VideoManager
{
    public partial class MainWindow : Window
    {
        private bool mouseButtonPushed = false;

        public MainWindow()
        {
            InitializeComponent();
            Icon = Extensions.EmbeddedImage(@"Resources\app_icon.ico");
            sidebar_toggle.Source = Extensions.EmbeddedImage(@"Resources\sidebar_icon.png");
            search_icon.Source = Extensions.EmbeddedImage(@"Resources\search_icon.png");
        }


        private void ToggleButtonMouseLeave(object sender, MouseEventArgs e)
        {
            mouseButtonPushed = false;
        }

        private void ToggleButtonMouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseButtonPushed = true;
        }

        private void ToggleButtonMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (mouseButtonPushed)
            {
                if (side_panel.Width.Value == 0) side_panel.Width = new GridLength(15, GridUnitType.Star);
                else side_panel.Width = new GridLength(0, GridUnitType.Star);
                mouseButtonPushed = false;
            }
        }

        private void OpenVideo(object sender, MouseButtonEventArgs e)
        {
            var info = new ProcessStartInfo((sender as FrameworkElement)?.Tag.ToString() ?? "");
            info.UseShellExecute = true;
            try { Process.Start(info); }
            catch (Win32Exception excpetion) { MessageBox.Show(excpetion.Message, "Windows error", MessageBoxButton.OKCancel, MessageBoxImage.Error); }
        }
    }

    public static class Extensions
    {
        public static string[] ImageFormats => new[] { ".png", ".jpg", ".bmp", ".webp", ".tiff" };
        public static string[] VideoFormats => new[] { ".mp4", ".mov", ".mkv", ".avi", ".wmv", ".webm", ".flv" };

        public static ImageSource EmbeddedImage(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream($"{typeof(Extensions).Namespace}.{resourceName.Replace(@"\", ".")}");
            if (stream != null && stream != Stream.Null)
                return BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            else
                throw new InvalidOperationException(nameof(resourceName));
        }

    }
}

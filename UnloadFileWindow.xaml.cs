using System.Collections.Generic;
using System.Windows;
using VideoManager.Models;

namespace VideoManager
{
    public partial class UnloadFileWindow : Window
    {
        private IList<Video> _videos;


        public UnloadFileWindow(IList<Video> videos)
        {
            InitializeComponent();
            Icon = Extensions.EmbeddedImage(@"Resources\app_icon.ico");
            _videos = videos;
            unload_control.ItemsSource = _videos;
        }


        private void UnloadVideos(object sender, RoutedEventArgs e)
        {
            foreach (var item in unload_control.SelectedItems)
                if (item is Video video)
                    _videos.Remove(video);

            Close();
        }
    }
}

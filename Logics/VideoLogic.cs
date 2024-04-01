using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using VideoManager.Models;
using VideoManager.Processes;
using VideoManager.Services;

namespace VideoManager.Logics
{
    public class VideoLogic : IVideoLogic
    {
        private IMessenger messenger;
        private IVideoUnloadService videoUnloadService;
        private IList<Video> allVideos;
        private IList<Video> selectedVideos;
        private OrderPreference orderPreference;
        private TitleFilter titleFilter;
        private Notification notification;

        public int VideoCount => allVideos.Count;
        public int SelectedVideoCount => selectedVideos.Count;


        public VideoLogic(IMessenger messenger, IVideoUnloadService videoUnloadService)
        {
            this.messenger = messenger;
            this.videoUnloadService = videoUnloadService;

            orderPreference = new OrderPreference();
            titleFilter = new TitleFilter();
        }


        public void OrderPreferenceChanged(OrderPreference orderPreference)
        {
            this.orderPreference.ChangeValues(orderPreference);
            UpdateVideos();
        }

        public void TitleFilterChanged(TitleFilter titleFilter)
        {
            this.titleFilter.ChangeValues(titleFilter);
            UpdateVideos();
        }

        public async Task LoadFiles()
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Select Files";
            dialog.Filter = $"Video Files|{string.Join(";", Extensions.VideoFormats.Select(t => "*" + t))}";
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                notification.Message = "Getting files...";
                notification.Color = Brushes.DarkGray;
                notification.Visibility = Visibility.Visible;
                messenger.Send("notification", "VideoInfo");

                await Task.Run(() => AddFiles(dialog.FileNames));

                notification.Message = string.Empty;
                notification.Color = Brushes.Black;
                notification.Visibility = Visibility.Hidden;
                messenger.Send("notification", "VideoInfo");
            }
        }

        public async Task LoadDirectory()
        {
            var dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                notification.Message = "Getting files...";
                notification.Color = Brushes.DarkGray;
                notification.Visibility = Visibility.Visible;
                messenger.Send("notification", "VideoInfo");

                await Task.Run(() => AddFiles(Directory.GetFiles(dialog.SelectedPath)
                    .Where(t => Extensions.VideoFormats.Contains(new FileInfo(t).Extension.ToLower())).ToArray()));

                notification.Message = string.Empty;
                notification.Color = Brushes.Black;
                notification.Visibility = Visibility.Hidden;
                messenger.Send("notification", "VideoInfo");
            }
        }

        public async Task LoadDirectories()
        {
            var dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                notification.Message = "Getting files...";
                notification.Color = Brushes.DarkGray;
                notification.Visibility = Visibility.Visible;
                messenger.Send("notification", "VideoInfo");

                await Task.Run(() => AddFiles(GetSubDirectoryFiles(dialog.SelectedPath).ToArray()));

                notification.Message = string.Empty;
                notification.Color = Brushes.Black;
                notification.Visibility = Visibility.Hidden;
                messenger.Send("notification", "VideoInfo");
            }
        }

        public void UnloadFiles()
        {
            videoUnloadService.UnloadVideos(allVideos);
        }

        public void Setup(IList<Video> allVideos, IList<Video> selectedVideos, ref Notification notification)
        {
            this.allVideos = allVideos;
            this.selectedVideos = selectedVideos;
            this.notification = notification;
        }

        public void UpdateVideos()
        {
            var temp = orderPreference.Orientation == OrderPreference.OrderOrientation.DESCENDING
               ? allVideos.Where(t => titleFilter.Filter.Invoke(t)).OrderByDescending(u => u, orderPreference.Comparer)
               : allVideos.Where(t => titleFilter.Filter.Invoke(t)).OrderBy(u => u, orderPreference.Comparer);

            selectedVideos.Clear();
            foreach (var item in temp)
                selectedVideos.Add(item);

            messenger.Send("videos updated", "VideoInfo");
        }


        private IEnumerable<string> GetSubDirectoryFiles(string directory)
        {
            var files = Directory.GetFiles(directory).Where(t => Extensions.VideoFormats.Contains(Path.GetExtension(t)));

            foreach (var dir in Directory.GetDirectories(directory))
                files = files.Union(GetSubDirectoryFiles(dir));

            return files;
        }

        private void AddFiles(params string[] files)
        {
            notification.Message = string.Empty;
            notification.Color = Brushes.Goldenrod;
            notification.Visibility = Visibility.Visible;

            for (int i = 0; i < files.Length; i++)
            {
                notification.Message = $"( {i + 1} / {files.Length} ) Loading file '{files[i]}'";
                messenger.Send("notification", "VideoInfo");

                var video = new Video(files[i]);

                if (!allVideos.Contains(video))
                    allVideos.Add(video);
            }

            notification.Message = "Files are loaded, videos are getting filtered and ordered...";
            notification.Color = Brushes.LimeGreen;

            System.Windows.Application.Current.Dispatcher.Invoke(() => messenger.Send("notification", "VideoInfo"),
                System.Windows.Threading.DispatcherPriority.ContextIdle);

            Task.Delay(100);

            System.Windows.Application.Current.Dispatcher.Invoke(() => UpdateVideos(),
                System.Windows.Threading.DispatcherPriority.ContextIdle);

            notification.Message = string.Empty;
            notification.Color = Brushes.Black;
            notification.Visibility = Visibility.Hidden;
            messenger.Send("notification", "VideoInfo");
        }
    }
}

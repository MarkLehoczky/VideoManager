using System.Collections.Generic;
using System.Linq;
using VideoManager.Models;
using VideoManager.Windows;

namespace VideoManager.Services
{
    public class VideoUnloadService : IVideoUnloadService
    {
        public void UnloadVideos(params Video[] items) => UnloadVideos(items.ToList());

        public void UnloadVideos(IList<Video> filePaths)
        {
            new UnloadFileWindow(filePaths).ShowDialog();
        }
    }
}

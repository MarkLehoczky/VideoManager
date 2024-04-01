using System.Collections.Generic;
using System.Threading.Tasks;
using VideoManager.Models;
using VideoManager.Processes;

namespace VideoManager.Logics
{
    public interface IVideoLogic
    {
        int VideoCount { get; }
        int SelectedVideoCount { get; }

        void OrderPreferenceChanged(OrderPreference selectedOrderPreference);
        void TitleFilterChanged(TitleFilter titleFilter);
        Task LoadFiles();
        Task LoadDirectory();
        Task LoadDirectories();
        void UnloadFiles();
        void Setup(IList<Video> allVideos, IList<Video> selectedVideos, ref Notification notification);
        void UpdateVideos();
    }
}
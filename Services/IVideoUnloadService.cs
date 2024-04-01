using System.Collections.Generic;
using VideoManager.Models;

namespace VideoManager.Services
{
    public interface IVideoUnloadService
    {
        void UnloadVideos(IList<Video> items);
    }
}

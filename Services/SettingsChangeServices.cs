using VideoManager.Windows;

namespace VideoManager.Services
{
    public class DateSettingsChangeService : IDateSettingsChangeService
    {
        public void ChangeSettings()
        {
            new DateSettingsWindow().ShowDialog();
        }
    }

    public class ThumbnailSettingsChangeService : IThumbnailSettingsChangeService
    {
        public void ChangeSettings()
        {
            new ThumbnailSettingsWindow().ShowDialog();
        }
    }

    public class TitleSettingsChangeService : ITitleSettingsChangeService
    {
        public void ChangeSettings()
        {
            new TitleSettingsWindow().ShowDialog();
        }
    }

    public class BackgroundSettingsChangeService : IBackgroundSettingsChangeService
    {
        public void ChangeSettings()
        {
            new BackgroundSettingsWindow().ShowDialog();
        }
    }
}

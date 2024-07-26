using CommunityToolkit.Mvvm.Messaging;
using VideoManager.Services;

namespace VideoManager.Logics
{
    public class SettingsLogic : ISettingsLogic
    {
        private IMessenger messenger;
        private IThumbnailSettingsChangeService thumbnailSettingsChangeService;
        private ITitleSettingsChangeService titleSettingsChangeService;
        private IDateSettingsChangeService dateSettingsChangeService;
        private IBackgroundSettingsChangeService backgroundSettingsChangeService;

        public SettingsLogic(IMessenger messenger, IThumbnailSettingsChangeService thumbnailSettingsChangeService, ITitleSettingsChangeService titleSettingsChangeService, IDateSettingsChangeService dateSettingsChangeService, IBackgroundSettingsChangeService backgroundSettingsChangeService)
        {
            this.messenger = messenger;
            this.thumbnailSettingsChangeService = thumbnailSettingsChangeService;
            this.titleSettingsChangeService = titleSettingsChangeService;
            this.dateSettingsChangeService = dateSettingsChangeService;
            this.backgroundSettingsChangeService = backgroundSettingsChangeService;
        }


        public void ChangeThumbnail()
        {
            thumbnailSettingsChangeService.ChangeSettings();
            messenger.Send("settings", "ThumbnailChange");
        }

        public void ChangeTitle()
        {
            titleSettingsChangeService.ChangeSettings();
            messenger.Send("settings", "TitleChange");
        }

        public void ChangeDate()
        {
            dateSettingsChangeService.ChangeSettings();
            messenger.Send("settings", "DatelChange");
        }

        public void ChangeBackground()
        {
            backgroundSettingsChangeService.ChangeSettings();
            messenger.Send("settings", "BackgroundChange");
        }
    }
}

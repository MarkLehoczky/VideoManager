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

        public SettingsLogic(IMessenger messenger, IThumbnailSettingsChangeService thumbnailSettingsChangeService, ITitleSettingsChangeService titleSettingsChangeService, IDateSettingsChangeService dateSettingsChangeService)
        {
            this.messenger = messenger;
            this.thumbnailSettingsChangeService = thumbnailSettingsChangeService;
            this.titleSettingsChangeService = titleSettingsChangeService;
            this.dateSettingsChangeService = dateSettingsChangeService;
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
    }
}

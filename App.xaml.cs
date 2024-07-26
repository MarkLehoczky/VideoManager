using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using VideoManager.Logics;
using VideoManager.Services;

namespace VideoManager
{
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    .AddSingleton<IVideoLogic, VideoLogic>()
                    .AddSingleton<ISettingsLogic, SettingsLogic>()
                    .AddSingleton<IVideoUnloadService, VideoUnloadService>()
                    .AddSingleton<IThumbnailSettingsChangeService, ThumbnailSettingsChangeService>()
                    .AddSingleton<ITitleSettingsChangeService, TitleSettingsChangeService>()
                    .AddSingleton<IDateSettingsChangeService, DateSettingsChangeService>()
                    .AddSingleton<IBackgroundSettingsChangeService, BackgroundSettingsChangeService>()
                    .AddSingleton<IMessenger>(WeakReferenceMessenger.Default)
                    .BuildServiceProvider());
        }
    }
}

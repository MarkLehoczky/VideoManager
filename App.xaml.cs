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
                    .AddSingleton<IVideoUnloadService, VideoUnloadService>()
                    .AddSingleton<IMessenger>(WeakReferenceMessenger.Default)
                    .BuildServiceProvider());
        }
    }
}

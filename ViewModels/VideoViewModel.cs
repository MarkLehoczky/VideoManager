using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VideoManager.Logics;
using VideoManager.Models;
using VideoManager.Processes;

namespace VideoManager.ViewModels
{
    public class VideoViewModel : ObservableRecipient
    {
        private IVideoLogic logic;
        private Notification notification;
        private OrderPreference selectedOrderPreference;
        private string titleFilterText;
        private IList<Task> tasks;

        public Notification Notification { get => notification; }
        public List<Video> Videos { get; set; }
        public ObservableCollection<Video> SelectedVideos { get; set; }
        public List<OrderPreference> OrderPreferences { get; set; }

        public OrderPreference SelectedOrderPreference
        {
            get { return selectedOrderPreference; }
            set
            {
                SetProperty(ref selectedOrderPreference, value);
                logic.OrderPreferenceChanged(selectedOrderPreference);
            }
        }
        public string TitleFilterText
        {
            get => titleFilterText;
            set
            {
                SetProperty(ref titleFilterText, value);
                logic.TitleFilterChanged(new TitleFilter(titleFilterText));
            }
        }

        public int VideoCount { get => logic.VideoCount; }
        public int SelectedVideoCount { get => logic.SelectedVideoCount; }

        public ICommand LoadFilesCommand { get; set; }
        public ICommand LoadDirectoryCommand { get; set; }
        public ICommand LoadDirectoriesCommand { get; set; }
        public ICommand UnloadFilesCommand { get; set; }



        public VideoViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IVideoLogic>()) { }

        public VideoViewModel(IVideoLogic logic)
        {
            this.logic = logic;
            Videos = new List<Video>();
            SelectedVideos = new ObservableCollection<Video>();
            tasks = new List<Task>();

            IComparer<Video> titleComparer = Comparer<Video>.Create((left, right) => left.Title.CompareTo(right.Title));
            IComparer<Video> dateComparer = Comparer<Video>.Create((left, right) => left.Date.CompareTo(right.Date));
            IComparer<Video> lengthComparer = Comparer<Video>.Create((left, right) => left.Length.CompareTo(right.Length));

            OrderPreferences = new List<OrderPreference>
            {
                new("Title", titleComparer, OrderPreference.OrderOrientation.ASCENDING),
                new("Title", titleComparer, OrderPreference.OrderOrientation.DESCENDING),
                new("Date", dateComparer, OrderPreference.OrderOrientation.ASCENDING),
                new("Date", dateComparer, OrderPreference.OrderOrientation.DESCENDING),
                new("Length", lengthComparer, OrderPreference.OrderOrientation.ASCENDING),
                new("Length", lengthComparer, OrderPreference.OrderOrientation.DESCENDING),
            };
            selectedOrderPreference = OrderPreferences[0];
            notification = Notification.Singleton;

            logic.Setup(Videos, SelectedVideos, ref notification);


            LoadFilesCommand = new RelayCommand(
                () => { tasks.Add(logic.LoadFiles()); (UnloadFilesCommand as RelayCommand)?.NotifyCanExecuteChanged(); },
                () => true
                );

            LoadDirectoryCommand = new RelayCommand(
                () => { tasks.Add(logic.LoadDirectory()); (UnloadFilesCommand as RelayCommand)?.NotifyCanExecuteChanged(); },
                () => true
                );

            LoadDirectoriesCommand = new RelayCommand(
                () => { tasks.Add(logic.LoadDirectories()); (UnloadFilesCommand as RelayCommand)?.NotifyCanExecuteChanged(); },
                () => true
                );

            UnloadFilesCommand = new RelayCommand(
                () => { logic.UnloadFiles(); logic.UpdateVideos(); (UnloadFilesCommand as RelayCommand)?.NotifyCanExecuteChanged(); },
                () => Videos.Count > 0 && tasks.All(t => t.IsCompleted)
                );

            Messenger.Register<VideoViewModel, string, string>(this, "VideoInfo", (_, _) =>
                {
                    OnPropertyChanged(nameof(VideoCount));
                    OnPropertyChanged(nameof(SelectedVideoCount));
                    OnPropertyChanged(nameof(Notification));
                });
        }

        private static bool IsInDesignMode => (bool)DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement)).Metadata.DefaultValue;
    }
}

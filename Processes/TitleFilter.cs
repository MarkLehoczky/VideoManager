using CommunityToolkit.Mvvm.ComponentModel;
using System;
using VideoManager.Models;

namespace VideoManager.Processes
{
    public class TitleFilter : ObservableObject
    {
        public string Title { get; set; }
        public Predicate<Video> Filter { get => FilterMethod; }

        public TitleFilter()
        {
            Title = string.Empty;
        }
        public TitleFilter(string title)
        {
            Title = title;
        }


        public void ChangeValues(string title)
        {
            Title = title;
        }
        public void ChangeValues(TitleFilter titleFilter)
        {
            Title = titleFilter.Title;
        }


        protected virtual bool FilterMethod(Video value)
        {
            return value.Title.Contains(Title, StringComparison.OrdinalIgnoreCase);
        }
    }
}

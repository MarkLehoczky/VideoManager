using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using VideoManager.Models;

namespace VideoManager.Processes
{
    public class OrderPreference : ObservableObject
    {
        public enum OrderOrientation { ASCENDING, DESCENDING }

        public string Name { get; set; }
        public IComparer<Video> Comparer { get; set; }
        public OrderOrientation Orientation { get; set; }

        public OrderPreference()
        {
            Name = string.Empty;
            Comparer = Comparer<Video>.Default;
            Orientation = OrderOrientation.ASCENDING;
        }
        public OrderPreference(string name, IComparer<Video> comparer, OrderOrientation sortOrientation)
        {
            Name = name;
            Comparer = comparer;
            Orientation = sortOrientation;
        }


        public void ChangeValues(string name, IComparer<Video> comparer, OrderOrientation sortOrientation)
        {
            Name = name;
            Comparer = comparer;
            Orientation = sortOrientation;
        }
        public void ChangeValues(OrderPreference orderPreference)
        {
            Name = orderPreference.Name;
            Comparer = orderPreference.Comparer;
            Orientation = orderPreference.Orientation;
        }
    }
}

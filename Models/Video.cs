using CommunityToolkit.Mvvm.ComponentModel;
using NReco.VideoConverter;
using NReco.VideoInfo;
using System;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

#nullable enable

namespace VideoManager.Models
{
    public class Video : ObservableObject, IEquatable<Video>, IComparable, IComparable<Video>, ICloneable
    {
        private string title;
        private DateTime date;
        private TimeSpan length;
        private string videoPath;
        private BitmapImage thumbnail;

        public string Title { get => title; private set => SetProperty(ref title, value); }
        public DateTime Date { get => date; private set => SetProperty(ref date, value); }
        public TimeSpan Length { get => length; private set => SetProperty(ref length, value); }
        public string VideoPath { get => videoPath; set => SetProperty(ref videoPath, value); }
        public BitmapImage Thumbnail { get => thumbnail; set => SetProperty(ref thumbnail, value); }


        public Video()
        {
            title = string.Empty;
            date = DateTime.MinValue;
            length = TimeSpan.Zero;
            videoPath = string.Empty;
            thumbnail = new BitmapImage();
        }
        public Video(Uri uri) : this(uri.AbsolutePath) { }
        public Video(string path) : this(new FileInfo(path)) { }
        public Video(FileInfo info) : this()
        {
            if (!info.Exists)
                throw new FileNotFoundException(info.FullName);
            else if (!Extensions.VideoFormats.Contains(info.Extension))
                throw new NotSupportedException(info.FullName);

            Title = Path.GetFileNameWithoutExtension(info.Name);
            Date = info.LastWriteTimeUtc;
            Length = new FFProbe().GetMediaInfo(info.FullName).Duration;
            VideoPath = info.FullName;

            Stream stream = Stream.Null;

            try
            {
                stream = new FileStream(Extensions.ImageFormats.First(t => Directory.GetFiles(info.DirectoryName).Any(u => Path.GetFileName(u) == Title + t)), FileMode.Open);
            }
            catch
            {
                stream = new MemoryStream();
                new FFMpegConverter().GetVideoThumbnail(info.FullName, stream, (int)(Length.TotalSeconds / 2));
                stream.Seek(0, SeekOrigin.Begin);
            }

            Thumbnail.BeginInit();
            Thumbnail.StreamSource = stream;
            Thumbnail.CacheOption = BitmapCacheOption.OnLoad;
            Thumbnail.EndInit();
            Thumbnail.Freeze();
        }

        public override string ToString()
        {
            return $"{Title} ({Date:yyyy. MMM. dd.}) [{Length:hh\\:mm\\:ss}]";
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Date, Length);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            else if (obj is Video video) return Equals(video);
            else return false;
        }
        public bool Equals(Video? other)
        {
            if (other == null) return false;
            else if (ReferenceEquals(this, other)) return true;
            else if (Title != other.Title) return false;
            else if (Date != other.Date) return false;
            else if (Length != other.Length) return false;
            else return true;
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) return -1;
            else if (obj is Video video) return CompareTo(video);
            else throw new ArgumentException($"{nameof(obj)} is not {nameof(Video)}");
        }
        public int CompareTo(Video? other)
        {
            if (other == null) return -1;
            else if (other == this) return 0;
            else if (Title.CompareTo(other.Title) != 0) return Title.CompareTo(other.Title);
            else if (Date.CompareTo(other.Date) != 0) return Date.CompareTo(other.Date);
            else if (Length.CompareTo(other.Length) != 0) return Length.CompareTo(other.Length);
            else return 0;
        }

        public object Clone()
        {
            return new Video
            {
                Title = title,
                Date = date,
                Length = length,
                VideoPath = videoPath,
                Thumbnail = thumbnail,
            };
        }
    }
}

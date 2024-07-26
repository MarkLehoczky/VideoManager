using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VideoManager
{
    public partial class MainWindow : Window
    {
        private bool mouseButtonPushed = false;

        public MainWindow()
        {
            InitializeComponent();
            Icon = Extensions.EmbeddedImage(@"Resources\app_icon.ico");
            sidebar_toggle.Source = Extensions.EmbeddedImage(@"Resources\sidebar_icon.png");
            search_icon.Source = Extensions.EmbeddedImage(@"Resources\search_icon.png");
        }


        private void ToggleButtonMouseLeave(object sender, MouseEventArgs e)
        {
            mouseButtonPushed = false;
        }

        private void ToggleButtonMouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseButtonPushed = true;
        }

        private void ToggleButtonMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (mouseButtonPushed)
            {
                if (side_panel.Width.Value == 0) side_panel.Width = new GridLength(15, GridUnitType.Star);
                else side_panel.Width = new GridLength(0, GridUnitType.Star);
                mouseButtonPushed = false;
            }
        }

        private void OpenVideo(object sender, MouseButtonEventArgs e)
        {
            var info = new ProcessStartInfo((sender as FrameworkElement)?.Tag.ToString() ?? "");
            info.UseShellExecute = true;
            try { Process.Start(info); }
            catch (Win32Exception excpetion) { MessageBox.Show(excpetion.Message, "Windows error", MessageBoxButton.OKCancel, MessageBoxImage.Error); }
        }
    }

    public static class Extensions
    {
        public static string[] ImageFormats => new[] { ".png", ".jpg", ".bmp", ".webp", ".tiff" };
        public static string[] VideoFormats => new[] { ".mp4", ".mov", ".mkv", ".avi", ".wmv", ".webm", ".flv" };

        public static ImageSource EmbeddedImage(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream($"{typeof(Extensions).Namespace}.{resourceName.Replace(@"\", ".")}");
            if (stream != null && stream != Stream.Null)
                return BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            else
                throw new InvalidOperationException(nameof(resourceName));
        }

        public static IEnumerable<FontFamily> FontFamilies => Fonts.SystemFontFamilies;
        public static IEnumerable<FontWeight> FontWeights => new List<FontWeight>
        {
            System.Windows.FontWeights.Black,
            System.Windows.FontWeights.Bold,
            System.Windows.FontWeights.DemiBold,
            System.Windows.FontWeights.ExtraBlack,
            System.Windows.FontWeights.ExtraBold,
            System.Windows.FontWeights.ExtraLight,
            System.Windows.FontWeights.Heavy,
            System.Windows.FontWeights.Light,
            System.Windows.FontWeights.Medium,
            System.Windows.FontWeights.Normal,
            System.Windows.FontWeights.Regular,
            System.Windows.FontWeights.SemiBold,
            System.Windows.FontWeights.Thin,
            System.Windows.FontWeights.UltraBlack,
            System.Windows.FontWeights.UltraBold,
            System.Windows.FontWeights.UltraLight,
        };
        public static IEnumerable<Brush> FontColors => new List<Brush>
        {
            Brushes.AliceBlue,
            Brushes.PaleGoldenrod,
            Brushes.Orchid,
            Brushes.OrangeRed,
            Brushes.Orange,
            Brushes.OliveDrab,
            Brushes.Olive,
            Brushes.OldLace,
            Brushes.Navy,
            Brushes.NavajoWhite,
            Brushes.Moccasin,
            Brushes.MistyRose,
            Brushes.MintCream,
            Brushes.MidnightBlue,
            Brushes.MediumVioletRed,
            Brushes.MediumTurquoise,
            Brushes.MediumSpringGreen,
            Brushes.MediumSlateBlue,
            Brushes.LightSkyBlue,
            Brushes.LightSlateGray,
            Brushes.LightSteelBlue,
            Brushes.LightYellow,
            Brushes.Lime,
            Brushes.LimeGreen,
            Brushes.PaleGreen,
            Brushes.Linen,
            Brushes.Maroon,
            Brushes.MediumAquamarine,
            Brushes.MediumBlue,
            Brushes.MediumOrchid,
            Brushes.MediumPurple,
            Brushes.MediumSeaGreen,
            Brushes.Magenta,
            Brushes.PaleTurquoise,
            Brushes.PaleVioletRed,
            Brushes.PapayaWhip,
            Brushes.SlateGray,
            Brushes.Snow,
            Brushes.SpringGreen,
            Brushes.SteelBlue,
            Brushes.Tan,
            Brushes.Teal,
            Brushes.SlateBlue,
            Brushes.Thistle,
            Brushes.Transparent,
            Brushes.Turquoise,
            Brushes.Violet,
            Brushes.Wheat,
            Brushes.White,
            Brushes.WhiteSmoke,
            Brushes.Tomato,
            Brushes.LightSeaGreen,
            Brushes.SkyBlue,
            Brushes.Sienna,
            Brushes.PeachPuff,
            Brushes.Peru,
            Brushes.Pink,
            Brushes.Plum,
            Brushes.PowderBlue,
            Brushes.Purple,
            Brushes.Silver,
            Brushes.Red,
            Brushes.RoyalBlue,
            Brushes.SaddleBrown,
            Brushes.Salmon,
            Brushes.SandyBrown,
            Brushes.SeaGreen,
            Brushes.SeaShell,
            Brushes.RosyBrown,
            Brushes.Yellow,
            Brushes.LightSalmon,
            Brushes.LightGreen,
            Brushes.DarkRed,
            Brushes.DarkOrchid,
            Brushes.DarkOrange,
            Brushes.DarkOliveGreen,
            Brushes.DarkMagenta,
            Brushes.DarkKhaki,
            Brushes.DarkGreen,
            Brushes.DarkGray,
            Brushes.DarkGoldenrod,
            Brushes.DarkCyan,
            Brushes.DarkBlue,
            Brushes.Cyan,
            Brushes.Crimson,
            Brushes.Cornsilk,
            Brushes.CornflowerBlue,
            Brushes.Coral,
            Brushes.Chocolate,
            Brushes.AntiqueWhite,
            Brushes.Aqua,
            Brushes.Aquamarine,
            Brushes.Azure,
            Brushes.Beige,
            Brushes.Bisque,
            Brushes.DarkSalmon,
            Brushes.Black,
            Brushes.Blue,
            Brushes.BlueViolet,
            Brushes.Brown,
            Brushes.BurlyWood,
            Brushes.CadetBlue,
            Brushes.Chartreuse,
            Brushes.BlanchedAlmond,
            Brushes.DarkSeaGreen,
            Brushes.DarkSlateBlue,
            Brushes.DarkSlateGray,
            Brushes.HotPink,
            Brushes.IndianRed,
            Brushes.Indigo,
            Brushes.Ivory,
            Brushes.Khaki,
            Brushes.Lavender,
            Brushes.Honeydew,
            Brushes.LavenderBlush,
            Brushes.LemonChiffon,
            Brushes.LightBlue,
            Brushes.LightCoral,
            Brushes.LightCyan,
            Brushes.LightGoldenrodYellow,
            Brushes.LightGray,
            Brushes.LawnGreen,
            Brushes.LightPink,
            Brushes.GreenYellow,
            Brushes.Gray,
            Brushes.DarkTurquoise,
            Brushes.DarkViolet,
            Brushes.DeepPink,
            Brushes.DeepSkyBlue,
            Brushes.DimGray,
            Brushes.DodgerBlue,
            Brushes.Green,
            Brushes.Firebrick,
            Brushes.ForestGreen,
            Brushes.Fuchsia,
            Brushes.Gainsboro,
            Brushes.GhostWhite,
            Brushes.Gold,
            Brushes.Goldenrod,
            Brushes.FloralWhite,
            Brushes.YellowGreen,
        };
        public static IEnumerable<TextWrapping> TextWrappings => new List<TextWrapping>
        {
            TextWrapping.WrapWithOverflow,
            TextWrapping.NoWrap,
            TextWrapping.Wrap,
        };
        public static IEnumerable<TextTrimming> TextTrimmings => new List<TextTrimming>
        {
            TextTrimming.None,
            TextTrimming.CharacterEllipsis,
            TextTrimming.WordEllipsis,
        };

        public static IEnumerable<HorizontalAlignment> HorizontalAlignments => new List<HorizontalAlignment>
        {
            HorizontalAlignment.Left,
            HorizontalAlignment.Center,
            HorizontalAlignment.Right,
            HorizontalAlignment.Stretch,
        };
    }
}

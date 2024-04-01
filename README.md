# Video Manager Program

## Description

This is a simple **video manager program** built in *Visual Studio* using C# 6.0, and WPF. It allows the users to use a user interface similar to *YouTube*. The program provides some simple filtering and ordering mode, loading- and unloading video files from selected paths and opening the selected video using the default video viewer software. Additionally it contains a collapsible side panel which can be used for further options.

## Features

- Load videos selected from the Windows file / folder explorer
- Acquire the *title*, *length*, *last write date* of the video from the file meta-data
- Order videos based on their *title*, *date* or *length*
- Filter videos based on their *title*
- Total- and selected video counter

## How To Use

### Console

```shell
# 1. Clone the Github repository
git clone https://github.com/MarkLehoczky/VideoManager.git

# 2. Build and run the project
dotnet run --project .\VideoManager\VideoManager.csproj --configuration Release
```

### Visual Studio

1. Click the `Code` button
2. Click the `Open with Visual Studio` button
3. Click the *Build and run* button in **Visual Studio**

### Binary file

1. Go to the [*Releases*](https://github.com/MarkLehoczky/VideoManager/releases) section.
2. Click the `VideoManager.exe` file to download.
3. Run the downloaded file.

## Prerequisites

- .NET 6.0
- Windows 7 or newer version

## Dependencies

- [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet) (v8.2.0)
- [Microsoft.Extensions.DependencyInjection](https://dot.net/) (v8.0.0)
- [NReco.VideoConverter](https://www.nrecosite.com/video_converter_net.aspx) (v1.2.0)
- [NReco.VideoInfo](https://www.nrecosite.com/video_info_net.aspx) (v1.2.0)

## License

This project is licensed under [MIT License](LICENSE).
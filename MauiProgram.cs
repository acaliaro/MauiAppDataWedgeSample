using CommunityToolkit.Maui;
using MauiAppDataWedgeSample.Services;
using MauiAppDataWedgeSample.ViewModels;
using Microsoft.Extensions.Logging;

namespace MauiAppDataWedgeSample;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<IDataWedgeService, DataWedgeService>();

        builder.Services.AddTransient<MainPage, MainViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

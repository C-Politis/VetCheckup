using Blazored.Modal;
using CanineCheckup.ViewModels.Dog;
using CanineCheckup.ViewModels.Modal;
using Microsoft.Extensions.Logging;

namespace CanineCheckup
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddBlazoredModal();
            builder.Services.AddSingleton<AddDogModalViewModel>();
            builder.Services.AddSingleton<DogOverviewViewModel>();
            builder.Services.AddSingleton<DogViewModel>();

            return builder.Build();
        }
    }
}

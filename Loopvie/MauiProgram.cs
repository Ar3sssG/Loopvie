﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using WordLoop.Extensions;
using WordLoop.ViewModels;
using Microsoft.Extensions.Configuration.Json;

namespace WordLoop
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.appsettings.json");

            var configuration = new ConfigurationBuilder()
                .AddJsonStream(stream).Build();

            builder.Configuration.AddConfiguration(configuration);

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.ConfigureServices(builder.Configuration);
            
            builder.Services.AddViewModels();

            builder.Services.AddPages();

            return builder.Build();
        }
    }
}

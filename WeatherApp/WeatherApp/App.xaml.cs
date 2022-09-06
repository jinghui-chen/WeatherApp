// <copyright file="App.cs" company="My Company Name">
// Copyright (c) 2022 All Rights Reserved
// </copyright>

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
                .WriteTo.File("Weather.log", rollingInterval: RollingInterval.Day);
                builder.AddSerilog(loggerConfiguration.CreateLogger());
            });

            ILogger<WeatherApiRestClient> logger = loggerFactory.CreateLogger<WeatherApiRestClient>();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            serviceCollection.AddSingleton<IConfiguration>(Configuration);
            serviceCollection.AddSingleton(typeof(ILogger<WeatherApiRestClient>), logger);
            
            IWeatherApiRestClient client = new WeatherApiRestClient(logger, Configuration);
            serviceCollection.AddSingleton(typeof(IWeatherApiRestClient), client);
            
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.AddTransient(typeof(MainWindow));
        }
    }
}

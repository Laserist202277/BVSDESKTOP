using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVM.HostBuilders;
using WPFDEMONSTRATIONAPP;

namespace MVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddConfiguration()
                .AddDbContext();
        }

        public IHost GetHost()
        {
            return _host;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            DbContextFactory contextFactory = _host.Services.GetRequiredService<DbContextFactory>();
            using (NotesDbContext context = contextFactory.CreateDbContext())
            {
                context.Database.Migrate();
            }

            //Window window = _host.Services.GetRequiredService<MainWindow>();
            //            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}

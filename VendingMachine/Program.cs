using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using VendingMachine_Application;
using VendingMachine_Application.Interfaces;
using VendingMachine_Persistance;

namespace VendingMachine
{
    public class Program
    {

        private static void RegisterServices()
        {

            var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json")
                           .AddEnvironmentVariables();
            Configuration = builder.Build();



            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            var services = new ServiceCollection();
            services.AddSingleton(appSettings);
            services.AddSingleton<IServiceApp, ServiceApp>();
            services.AddSingleton<IHelper, Helper>();
            services.AddSingleton<IBlOrders, BlOrders>();
            _provider = services.BuildServiceProvider(true);


        }
        private static void DisposeServices()
        {
            if (_provider == null)
            {
                return;
            }
            if (_provider is IDisposable)
            {
                ((IDisposable)_provider).Dispose();
            }
        }


        public static IConfiguration Configuration { get; set; }
        private static IServiceProvider _provider;
        public static void Main(string[] args)
        {

            RegisterServices();

            var service = _provider.GetRequiredService<IServiceApp>();
            service.LoadItems();
            service.DoProcess();

            DisposeServices();

        }
    }
}

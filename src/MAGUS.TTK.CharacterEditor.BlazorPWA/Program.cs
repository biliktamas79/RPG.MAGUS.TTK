using MAGUS.TTK.Data;
using MAGUS.TTK.Domain;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MAGUS.TTK.CharacterEditor.BlazorPWA
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<IFileContentResolver>(serviceProvider =>
            {
                var httpClient = serviceProvider.GetRequiredService<HttpClient>();

                return new HttpFileContentResolver(httpClient, "ttk/084/definitions/");
            });

            var initializer = new MagusTtkContextServiceInitializer();
            initializer.RegisterServices(builder.Services);

            var host = builder.Build();//.RunAsync();

            //var dataInitializer = host.Services.GetRequiredService<IDataInitializer<MagusTtkContext>>();
            //var ctx = host.Services.GetRequiredService<MagusTtkContext>();
            //await dataInitializer.InitializeData(ctx);

            await host.RunAsync();
        }
    }
}

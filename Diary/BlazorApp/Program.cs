using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            // Http call to URL
            builder.Services.AddScoped(
                sp => new HttpClient
                {
                    BaseAddress = new Uri("https://api.open-meteo.com/v1/")
                });

            await builder.Build().RunAsync();

            // HTTP call is Base address of the project application
            /*
            builder.Services.AddScoped(
                sp => new HttpClient { 
                    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
            */
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace OpenWebTest
{
    ///<Summary>
    /// First class called in the app
    ///</Summary>
    public class Program
    {
        ///<Summary>
        /// First method 
        ///</Summary>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        ///<Summary>
        /// Host Builder
        ///</Summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

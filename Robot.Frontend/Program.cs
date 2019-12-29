using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Robot.Frontend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) => {
                    if (!Uri.TryCreate(Assembly.GetExecutingAssembly().CodeBase, UriKind.RelativeOrAbsolute, out var uri)) {
                        throw new Exception("Cannot determine file system path of executing assembly");
                    }
                    var path = Path.GetDirectoryName(uri.LocalPath);
                    if (string.IsNullOrEmpty(path)) {
                        throw new Exception("Cannot determine file system path of executing assembly");
                    }
                    config.AddJsonFile(Path.Combine(path, "hosting.json"), optional:true, reloadOnChange:true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

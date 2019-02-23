using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MyB2B.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args);
#if DEBUG
            builder.UseSetting("ConnectionString", "Server=localhost\\SQLEXPRESS;Database=MyB2B;Integrated Security=True;MultipleActiveResultSets=True;");
#endif
                builder.UseStartup<Startup>();
                return builder;
        }

    }
}

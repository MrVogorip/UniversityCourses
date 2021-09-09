using Serilog;
using Serilog.Core;
using System.Web.Hosting;

namespace TourAgency.Web.Helpers
{
    public static class SLogger
    {
        private static readonly Logger logger;
        static SLogger()
        {
            var logPath = HostingEnvironment.MapPath($"~/Logs/log.txt");
            logger = new LoggerConfiguration().WriteTo.File(logPath).CreateLogger();
        }
        public static void StartLog()
        {
            logger.Information($"Logger On");
        }
        public static void InfoToFile(string info)
        {
            logger.Information(info);
        }
    }
}
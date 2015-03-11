using System;
using System.Configuration;
using System.IO;

namespace TimeTracking
{
    public static class ConfigSettings
    {
        public static string TimeTrackingFile
        {
            get
            {
                var ttf = ConfigurationManager.AppSettings["timetrackingfile"];

                if (string.IsNullOrWhiteSpace(ttf))
                    ttf = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "timetracking.xml");

                return ttf;
            }
        }
    }
}
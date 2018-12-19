using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace S3BotCmd
{
    public static class Configuration
    {
        public static string BotToken;
        public static string AttendanceSheetUrl;

        public static void Initialize(IConfigurationRoot config)
        {
            BotToken = config["BotToken"];
            AttendanceSheetUrl = config["AttendanceSheetUrl"];
        }
    }
}

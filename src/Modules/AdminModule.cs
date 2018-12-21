using Discord.Commands;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using S3BotCmd.Preconditions;
namespace S3BotCmd.Modules
{
    [Group("admin"), RequireProperChannel]
    public class AdminModule : ModuleBase
    {
        // ~admin absences 12/20/2018
        [Command("absences")]
        public async Task GetListOfAbsentMembers(DateTime date)
        {
            Console.WriteLine(string.Format("Received absences request for date {0} from channel {1} from user {2}", date.ToString(), Context.Channel.Name, Context.User.Username));

            Console.WriteLine("Looking up absence sheet");
            HttpClient client = new HttpClient();
            var sheetResponse = await client.GetAsync(Configuration.AttendanceSheetUrl);
            JObject attendanceSheet = JObject.Parse(await sheetResponse.Content.ReadAsStringAsync());
            var responses = attendanceSheet["values"].Where(r => r.HasValues); // Can receive null cells from deleted entries somehow
            var sb = new StringBuilder();

            foreach (var response in responses)
            {
                var responseObj = new AttendanceResponse(response);

                if (responseObj.DateAbsent.ToShortDateString() == date.ToShortDateString())
                {
                    sb.AppendLine(string.Format("Name: {0} | Date: {1}", responseObj.Name, responseObj.DateAbsent.ToShortDateString()));
                }
            }

            Console.WriteLine("Replying");
            
            // Todo: Kind of hacky
            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                await ReplyAsync(sb.ToString());
            }
            else
            {
                await ReplyAsync("No results found");
            }
        }

        private class AttendanceResponse
        {
            public DateTime TimeSubmitted;
            public DateTime DateAbsent;
            public string Name;
            public string Reason;

            public AttendanceResponse(JToken rawResponse)
            {
                // Windows/linux variants as dotnetcore uses system time
                try
                {
                    this.TimeSubmitted = TimeZoneInfo.ConvertTimeToUtc(DateTime.Parse(rawResponse[0].ToString()), TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"));
                }
                catch (TimeZoneNotFoundException)
                {
                    //Should work on linux
                    this.TimeSubmitted = TimeZoneInfo.ConvertTimeToUtc(DateTime.Parse(rawResponse[0].ToString()), TimeZoneInfo.FindSystemTimeZoneById("America/Tijuana"));
                }

                this.DateAbsent = DateTime.Parse(rawResponse[3].ToString());
                this.Name = rawResponse[1].ToString();
                this.Reason = rawResponse[4].ToString();
            }
        }
    }
}

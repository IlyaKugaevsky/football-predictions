using Predictions.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Predictions.Services
{
    public class FileService
    {
        private readonly string _defaultFolder = "~/App_Data/TextFiles";
        private readonly string _tourSchedulePannern = @"^(?<date>\d\d\.\d\d\.\d\d\d\d)" + @"\|"
                            + @"(?<time>\d\d:\d\d)" + @"(?<spaces>\s+)"
                            + @"(?<homeTeam>\w+(\s\w+)?)" + @"(?<trash>(\W|_)+)"
                            + @"(?<awayTeam>(\w+)(\s\w*)?)$";

        public List<MatchInfo> ReadTourMatches(string localFilePath = "")
        {
            var filePath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(_defaultFolder), localFilePath);
            //mb optimization, read line => handle, 1 cycle
            var lines = File.ReadAllLines(filePath);// System.Text.Encoding.UTF8
            var matchlist = new List<MatchInfo>();
            foreach (var line in lines)
            {
                var match = System.Text.RegularExpressions.Regex.Match(line, _tourSchedulePannern);
                Console.WriteLine("date: " + match.Groups["date"].Value + "\n"
                    + "time: " + match.Groups["time"].Value + "\n"
                    + "spaces: " + "|" + match.Groups["spaces"].Value + "|" + "\n"
                    + "homeTeam: " + match.Groups["homeTeam"].Value + "\n"
                    + "trash: " + match.Groups["trash"].Value + "\n"
                    + "awayTeam: " + match.Groups["awayTeam"].Value);

                DateTime date = DateTime.Parse(match.Groups["date"].Value + " " + match.Groups["time"]);
                string homeTeam = match.Groups["homeTeam"].Value;
                string awayTeam = match.Groups["awayTeam"].Value;
                matchlist.Add(new MatchInfo(date, homeTeam, awayTeam));
            }
            return matchlist;
        }

        public void TestWriteFile(string fileName)
        {
            var filePath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(_defaultFolder), fileName);
            string hello = "Hello!";
            var lines = new List<string>();
            lines.Add(hello);
            File.WriteAllLines(filePath, lines);
        }
    }
}
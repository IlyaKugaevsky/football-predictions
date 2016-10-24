using Predictions.ViewModels;
using Predictions.ViewModels.Basis;
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
        public readonly string TOUR_SCHEDULE_PATTERN = @"^(?<date>\d\d\.\d\d\.\d\d\d\d)" + @"\|"
                            + @"(?<time>\d\d:\d\d)" + @"(?<spaces>\s+)"
                            + @"(?<homeTeam>\w+(\s\w+)?)" + @"(?<trash>(\W|_)+)"
                            + @"(?<awayTeam>(\w+)(\s\w*)?)$";

        public readonly string PREDICTION_PATTERN = @"^(?<homeTeam>\w+(\s\w+)?)" + @"\s-\s"
                                                + @"(?<awayTeam>(\w+)(\s\w+)?)" + @"\s" + @"?<score>\d\d?:\d\d?";

        public List<MatchInfo> ReadTourMatches(string localFilePath = "")
        {
            var filePath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(_defaultFolder), localFilePath);
            //mb optimization, read line => handle, 1 cycle
            var lines = File.ReadAllLines(filePath);// System.Text.Encoding.UTF8
            var matchlist = new List<MatchInfo>();
            foreach (var line in lines)
            {
                var match = System.Text.RegularExpressions.Regex.Match(line, TOUR_SCHEDULE_PATTERN);
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

        public List<MatchInfo> ParseTourSchedule(string input)
        {
            var lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var matchlist = new List<MatchInfo>();
            foreach (var line in lines)
            {
                var match = System.Text.RegularExpressions.Regex.Match(line, TOUR_SCHEDULE_PATTERN);

                DateTime date = DateTime.Parse(match.Groups["date"].Value + " " + match.Groups["time"].Value);
                string homeTeam = match.Groups["homeTeam"].Value;
                string awayTeam = match.Groups["awayTeam"].Value;
                matchlist.Add(new MatchInfo(date, homeTeam, awayTeam));
            }
            return matchlist;
        }

        public List<MatchInfo> ParseExpertPredictions(string input)
        {
            var lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var predictionlist = new List<PredictionInfo>();
            foreach (var line in lines)
            {
                var match = System.Text.RegularExpressions.Regex.Match(line, PREDICTION_PATTERN);

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
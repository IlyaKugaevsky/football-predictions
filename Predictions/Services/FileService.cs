using Predictions.ViewModels;
using Predictions.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Predictions.Models.Dtos;

namespace Predictions.Services
{
    public class FileService
    {
        private readonly string _defaultFolder = "~/App_Data/TextFiles";
        public readonly string TourSchedulePattern = @"^(?<date>\d\d\.\d\d\.\d\d\d\d)" + @"\|"
                            + @"(?<time>\d\d:\d\d)" + @"(?<spaces>\s+)"
                            + @"(?<homeTeam>\w+(\s\w+)?)" + @"(?<trash>(\W|_)+)"
                            + @"(?<awayTeam>(\w+)(\s\w*)?)$";

        public readonly string PredictionPattern = @"^(?<homeTeam>\w+(\s\w+)?)" + @"\s-\s"
                                                + @"(?<awayTeam>(\w+)(\s\w+)?)" + @"\s" + @"(?<score>\d\d?:\d\d?)" + @"(?<spaces>\s*)$";

        public List<ParsingMatchInfo> ReadTourMatches(string localFilePath = "")
        {
            var filePath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(_defaultFolder), localFilePath);
            //mb optimization, read line => handle, 1 cycle
            var lines = File.ReadAllLines(filePath);// System.Text.Encoding.UTF8
            var matchlist = new List<ParsingMatchInfo>();
            foreach (var line in lines)
            {
                var match = System.Text.RegularExpressions.Regex.Match(line, TourSchedulePattern);
                Console.WriteLine("date: " + match.Groups["date"].Value + "\n"
                    + "time: " + match.Groups["time"].Value + "\n"
                    + "spaces: " + "|" + match.Groups["spaces"].Value + "|" + "\n"
                    + "homeTeam: " + match.Groups["homeTeam"].Value + "\n"
                    + "trash: " + match.Groups["trash"].Value + "\n"
                    + "awayTeam: " + match.Groups["awayTeam"].Value);

                DateTime date = DateTime.Parse(match.Groups["date"].Value + " " + match.Groups["time"]);
                string homeTeam = match.Groups["homeTeam"].Value;
                string awayTeam = match.Groups["awayTeam"].Value;
                matchlist.Add(new ParsingMatchInfo(date, homeTeam, awayTeam));
            }
            return matchlist;
        }

        public List<ParsingMatchInfo> ParseTourSchedule(string input)
        {
            var lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var matchlist = new List<ParsingMatchInfo>();
            foreach (var line in lines)
            {
                var match = System.Text.RegularExpressions.Regex.Match(line, TourSchedulePattern);

                DateTime date = DateTime.Parse(match.Groups["date"].Value + " " + match.Groups["time"].Value);
                string homeTeam = match.Groups["homeTeam"].Value;
                string awayTeam = match.Groups["awayTeam"].Value;
                matchlist.Add(new ParsingMatchInfo(date, homeTeam, awayTeam));
            }
            return matchlist;
        }

        //rewrite!!!
        public List<FootballScore> ParseExpertPredictions(string input, List<string> orderedTeamTitlelist)
        {
            var isCorrect = true; 

            var lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            lines = lines.Where(l => !string.IsNullOrWhiteSpace(l)).Distinct().ToList();
            if (2 * lines.Count() != orderedTeamTitlelist.Count()) isCorrect = false;

            var predictionlist = new List<FootballScore>();
            var i = 0;

            foreach (var line in lines)
            {
                var match = System.Text.RegularExpressions.Regex.Match(line, PredictionPattern);
                var teamplateCorrect = match.Success;
                var homeTeamCorrect = match.Groups["homeTeam"].Value.Equals(orderedTeamTitlelist[i]);
                var awayTeamCorerct = match.Groups["awayTeam"].Value.Equals(orderedTeamTitlelist[i + 1]);

                if (!match.Success || !homeTeamCorrect || !awayTeamCorerct)
                {
                    isCorrect = false;
                    break;
                }
                
                predictionlist.Add(new FootballScore(match.Groups["score"].Value));
                i += 2;
            }
            return isCorrect ? predictionlist : new List<FootballScore>();
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
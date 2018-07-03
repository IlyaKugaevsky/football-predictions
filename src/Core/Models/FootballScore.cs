using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.Models.Dtos;

namespace Core.Models
{
    public class FootballScore
    {
        private readonly string _score;

        public FootballScore()
        {
            _score = string.Empty;
        }

        public FootballScore(string input)
        {
            var rgx = new Regex(Pattern);

            if (rgx.IsMatch(input))
            {
                _score = input;
            }
            else
            {
                throw new ArgumentException("Invalide score.");
            }
        }

        public FootballScoreViewModel GenerateViewModel(bool editable, string editableEmptySign = "",
            string uneditableEmptySign = "-")
        {
            return new FootballScoreViewModel(_score, editable, editableEmptySign, uneditableEmptySign);
        }

        public override string ToString()
        {
            return _score;
        }

        public static readonly string Pattern = @"^$|^[0-9]{1,2}:[0-9]{1,2}$";
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Predictions.ViewModels.Basis
{
    public class FootballScore
    {
        private string _value;

        [Required]
        [RegularExpression(@"^$|^[0-9]{1,2}:[0-9]{1,2}$", ErrorMessage = "Некорректный счет")]
        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                //to think; "N/A", "-", "не сыгран"?
                _value = value;
            }
        }

        public int GetHomeGoals()
        {
            return Convert.ToInt32(this.Value.Substring(0, this.Value.IndexOf(':')));
        }

        public int GetAwayGoals()
        {
            return Convert.ToInt32(this.Value.Substring(this.Value.IndexOf(':') + 1, this.Value.Length - this.Value.IndexOf(':') - 1));
        }

        public int GetDifference(FootballScore expression)
        {
            return this.GetHomeGoals() - this.GetAwayGoals();
        }
    }
}
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

        public FootballScore()
        {
            _value = string.Empty;
        }

        public FootballScore(string value, bool editable = false)
        {
            _value = value;
            Editable = editable;
        }

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

        //editable in view
        public bool Editable { get; set; }
    }

}
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization.Formatters;
using Core.Helpers;

namespace Core.Models.Dtos
{
    public class FootballScoreViewModel
    {
        private string _editableEmptySign;
        private string _uneditableEmptySign;

        public FootballScoreViewModel()
        {
            Score = string.Empty;
            _editableEmptySign = "";
            _uneditableEmptySign = "-";
        }

        public FootballScoreViewModel(string score, bool editable, string editableEmptySign, string uneditableEmptySign)
        {
            //_score = score;
            if (score.IsNullOrEmpty()) Score = editable ? editableEmptySign : uneditableEmptySign;
            else Score = score;

            _editableEmptySign = editableEmptySign;
            _uneditableEmptySign = uneditableEmptySign;
            Editable = editable;
        }

        public FootballScoreViewModel(string score, bool editable = false)
        {
            Score = score;
            Editable = editable;
        }

        [RegularExpression(@"^$|^[0-9]{1,2}:[0-9]{1,2}$", ErrorMessage = "Некорректный счет")]
        public string Score { get; set; }

        public bool Editable { get; set; }
    }

}
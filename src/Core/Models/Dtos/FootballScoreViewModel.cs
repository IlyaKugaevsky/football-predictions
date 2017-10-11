using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization.Formatters;
using Core.Helpers;

namespace Core.Models.Dtos
{
    public class FootballScoreViewModel
    {
        private string _score;
        private bool _editable;

        private string _editableEmptySign;
        private string _uneditableEmptySign;

        public FootballScoreViewModel()
        {
            _score = string.Empty;
            _editableEmptySign = "";
            _uneditableEmptySign = "-";
        }

        public FootballScoreViewModel(string score, bool editable, string editableEmptySign, string uneditableEmptySign)
        {
            //_score = score;
            if (score.IsNullOrEmpty()) _score = editable ? editableEmptySign : uneditableEmptySign;
            else _score = score;

            _editableEmptySign = editableEmptySign;
            _uneditableEmptySign = uneditableEmptySign;
        }

        public FootballScoreViewModel(string score, bool editable = false)
        {
            _score = score;
            Editable = editable;
        }

        [RegularExpression(@"^$|^[0-9]{1,2}:[0-9]{1,2}$", ErrorMessage = "Некорректный счет")]
        public string Score
        {
            get
            {
                return _score;
            }
            set
            {
                //to think; "N/A", "-", "не сыгран"?
                _score = value;
            }
        }

        public bool Editable { get; set; }

        public void SetEditable()
        {
            if (!_editable) _editable = true;
        }

        public void SetUneditable()
        {
            if (!_editable) _editable = false;
        }
    }

}
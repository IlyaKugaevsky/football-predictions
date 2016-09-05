using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predictions.ViewModels
{
    public class EditableItem
    {
        public bool Editable { get; set; } 
        public string Item { get; set; }
    }

    public class MatchTableRow
    {
        public MatchTableRow()
        { }

        public MatchTableRow(MatchInfo matchInfo, EditableItem editableItem = null, Func<object, IHtmlString> content = null)
        {
            MatchInfo = matchInfo;
            EditableItem = editableItem;
            Content = content;
        }

        public MatchInfo MatchInfo {get; set;}
        public EditableItem EditableItem { get; set; } 
        public Func<object, IHtmlString> Content  { get; set; }  
    }

    public class MatchTableViewModel
    {
        public List<string> Headers { get; set; }
        public List<MatchTableRow> Rows { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intSoft.MVC.Core.Helpers.UiGrid
{
    public class UiGridColumnFilter
    {
        public UiGridColumnFilter()
        {
            Type = UiGridColumnFilterType.Input;
            Condition = UiGridColumnFilterCondition.StartsWith;
        }
        public string Term { get; set; }
        public string PlaceHolder { get; set; }
        public string AriaLabel { get; set; }

        /// <summary>
        /// Takes its value from UiGridColumnFilterType static class
        /// </summary>

        [DefaultValue(UiGridColumnFilterType.Input)]
        public string Type { get; set; }

        [DefaultValue(UiGridColumnFilterCondition.StartsWith)]
        public UiGridColumnFilterCondition Condition { get; set; }
        
        /// <summary>
        /// Should be filled only when the Type of the filter is UiGridColumnFilterType.Select
        /// </summary>
        [DefaultValue(null)]
        public List<UiGridColumnSelectOption> SelectOptions { get; set; }
    }
}

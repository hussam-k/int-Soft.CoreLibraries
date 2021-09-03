using System.ComponentModel;

namespace intSoft.MVC.Core.Helpers.UiGrid
{
    public class UiGridColumnSort
    {
        public UiGridColumnSort()
        {
            Direction = null;
            IgnoreSort = false;
            Priority = 0;
        }

        /// <summary>
        /// Takes its value from UiGridColumnSortDicrection static class
        /// </summary>
        [DefaultValue(null)]
        public string Direction { get; set; }
        
        /// <summary>
        /// says what order to sort the columns in (lower priority gets sorted first)
        /// </summary>
        [DefaultValue(0)]
        public int Priority { get; set; }

        /// <summary>
        /// if set to true this sort is ignored (used by tree to manipulate the sort functionality)
        /// </summary>
        [DefaultValue(false)]
        public bool IgnoreSort { get; set; }

    }
}

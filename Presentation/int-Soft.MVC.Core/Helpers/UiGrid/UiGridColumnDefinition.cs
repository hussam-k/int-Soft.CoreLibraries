using System.Collections.Generic;
using System.ComponentModel;
using intSoft.MVC.Core.Utilities;
using Newtonsoft.Json;

namespace intSoft.MVC.Core.Helpers.UiGrid
{
    public class UiGridColumnDefinition
    {
        public UiGridColumnDefinition(string field, string displayName = null)
        {
            Field = field;
            DisplayName = displayName ?? field;

            Filters = null;
            Width = null;
            Sort = null;
            CellFilter = null;
            Type = null;

            Visible = true;
            EnableSorting = true;
            EnableFiltering = true;
            EnableColumnResizing = true;
            EnablePinning = false;
            EnableCellEdit = false;
            EnableHiding = false;
            EnableColumnMenu = true;
            PinnedLeft = false;
            PinnedRight = false;
            ShowColumnFooter = false;
            ShowHeader = true;
            AggregationHideLabel = false;
            AggregationType = UiGridColumnAggregationType.Sum;
            CellTooltip = true;
        }
        
        public string DisplayName { get; set; }
        public string Field { get; set; }
        
        [DefaultValue(false)]
        public bool EnablePinning { get; set; }

        [DefaultValue(false)]
        public bool PinnedLeft { get; set; }

        [DefaultValue(false)]
        public bool PinnedRight { get; set; }

        [DefaultValue(true)]
        public bool Visible { get; set; }

        [DefaultValue(true)]
        public bool EnableSorting { get; set; }

        [DefaultValue(true)]
        public bool EnableFiltering { get; set; }

        [DefaultValue(true)]
        public bool EnableColumnResizing { get; set; }
        
        [DefaultValue(false)]
        public bool EnableCellEdit { get; set; }

        [DefaultValue(false)]
        public bool EnableHiding { get; set; }

        [DefaultValue(true)]
        public bool EnableColumnMenu { get; set; }
        
        [DefaultValue(false)]
        public bool ShowColumnFooter { get; set; }

        [DefaultValue(true)]
        public bool ShowHeader { get; set; }

        [DefaultValue(true)]
        public bool AggregationHideLabel { get; set; }

        [DefaultValue(null)]
        public string Type { get; set; }

        [DefaultValue(null)]
        public string CellFilter { get; set; }

        [DefaultValue(null)]
        public string CellClass { get; set; }

        [DefaultValue(null)]
        public string CellTemplate { get; set; }
        
        [DefaultValue(false)]
        public bool CellTooltip { get; set; }

        [DefaultValue(null)]
        public string HeaderCellTemplate { get; set; }
        
        [DefaultValue(UiGridColumnAggregationType.Sum)]
        public UiGridColumnAggregationType AggregationType { get; set; }
        
        [DefaultValue(null)]
        [JsonConverter(typeof(ToStringJsonConverter))]
        public UiGridColumnWidth Width { get; set; }

        [DefaultValue(null)]
        public UiGridColumnSort Sort { get; set; }

        [DefaultValue(null)]
        public List<UiGridColumnFilter> Filters { get; set; }
    }
}

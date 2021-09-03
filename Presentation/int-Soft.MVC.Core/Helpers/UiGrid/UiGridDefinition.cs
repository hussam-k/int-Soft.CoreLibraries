using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace intSoft.MVC.Core.Helpers.UiGrid
{
    public class UiGridDefinition
    {
        public UiGridDefinition()
        {
            EnableSorting = true;
            EnableColumnMenus = true;
            EnableColumnResizing = true;

            EnableFiltering = false;
            ShowColumnFooter = false;
            ShowGridFooter = false;
            ShowHeader = true;
            Pinnable = true;
            ColumnDefinitions = null;


            OnRegisterApi = "function (gridApi) { $scope.grid2Api = gridApi; }";
        }

        [DefaultValue(false)]
        public bool EnableFiltering { get; set; }

        [DefaultValue(true)]
        public bool EnableSorting { get; set; }

        [DefaultValue(true)]
        public bool EnableColumnMenus { get; set; }

        [DefaultValue(true)]
        public bool EnableColumnResizing { get; set; }
        
        [DefaultValue(false)]
        public bool ShowColumnFooter { get; set; }

        [DefaultValue(false)]
        public bool ShowGridFooter { get; set; }

        [DefaultValue(true)]
        public bool ShowHeader { get; set; }

        [DefaultValue(true)]
        public bool Pinnable { get; set; }

        [DefaultValue(null)]
        [JsonProperty(PropertyName = "columnDefs")]
        public List<UiGridColumnDefinition> ColumnDefinitions { get; set; }

        [DefaultValue(null)]
        public string OnRegisterApi { get; set; }
        
    }
}

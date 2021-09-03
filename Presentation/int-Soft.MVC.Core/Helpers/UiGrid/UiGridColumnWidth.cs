using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace intSoft.MVC.Core.Helpers.UiGrid
{
    public class UiGridColumnWidth
    {
        public UiGridColumnWidth(int value, UiGridUnitType unitType = UiGridUnitType.Absolute)
        {
            Value = value;
            UnitType = unitType;
        }

        public UiGridUnitType UnitType { get; set; }
        public int Value { get; set; }
        
        public override string ToString()
        {
            switch (UnitType)
            {
                case UiGridUnitType.Absolute:
                    return Value.ToString();
                case UiGridUnitType.Percentage:
                    return string.Format("{0}%", Value);
                case UiGridUnitType.Star:
                    return new string('*', Value);
            }

            return string.Empty;
        }
    }
}

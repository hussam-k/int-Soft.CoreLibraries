using System;
using intSoft.MVC.Core.Enumerations;

namespace intSoft.MVC.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayAtAttribute : Attribute
    {
        public DisplayAtAttribute()
        {
            DisplayAt = DisplayAt.All;
        }

        public DisplayAt DisplayAt { get; set; }
        public bool IsHiddenInput { get; set; }
    }
}
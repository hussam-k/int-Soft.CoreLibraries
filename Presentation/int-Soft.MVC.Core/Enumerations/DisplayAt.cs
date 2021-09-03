using System;

namespace intSoft.MVC.Core.Enumerations
{
    [Flags]
    public enum DisplayAt
    {
        None = 0,
        Editor = 1,
        Display = 2,
        Drafts = 4,
        List = 8,
        DisplayHeader = 16,
        All = 32
    }
}
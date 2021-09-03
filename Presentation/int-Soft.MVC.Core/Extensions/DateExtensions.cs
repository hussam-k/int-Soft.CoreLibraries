using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using intSoft.MVC.Core.HTTPApplication;

namespace intSoft.MVC.Core.Extensions
{
    public static class DateExtensions
    {
        public static string ToJavascriptDate(this DateTime dateTime)
        {
            return dateTime
                .Subtract(new DateTime(1970, 1, 1))
                .TotalMilliseconds
                .ToString();
        }
        public static string ToFormattedDate(this DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy", new CultureInfo("en-US"));
        }
    }
}
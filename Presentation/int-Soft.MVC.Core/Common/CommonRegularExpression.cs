namespace intSoft.MVC.Core.Common
{
    public static class CommonRegularExpression
    {
        public const string Email =
            @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$";

        public const string WebSite =
            @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$";

        public const string EgyptianMobileNumber = @"^\s*(\d{11})\s*$";
        public const string InternationalEgyptianMobileNumber = @"\b+201\d{9}\s*$";

        public const string EgyptianLandNumber = @"^\s*(\d{10})\s*$";

    }
}
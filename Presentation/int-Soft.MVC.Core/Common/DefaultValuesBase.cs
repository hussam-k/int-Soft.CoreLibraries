using System;

namespace intSoft.MVC.Core.Common
{
    public static class DefaultValuesBase
    {
        public static string SessionUserKey = "CurrentUser";
        public static string SessionUserOperationsKey = "UserOperations";
        public static string SessionUserRoleKey = "UserRoles";
        public static string SessionPhoneNumber = "PhoneNumber";
        public static string SessionPhoneVerificationCode = "PhoneVerificationCode";
        public static string SessionPhoneVerificationDate = "PhoneVerificationDate";
        public static string Admin = "admin";    
        public static string StructuremapNestedContainerKey = "Structuremap.Nested.Container";
        public static string AntiForgeryValidationKey = "RequestVerificationToken";
        public static string SessionUserPermissions = "UserPermissions";
        public static string ControllerNameSuffix = "Controller";
        public static string SaveAction = "Save";
        public static string ListAction = "List";
        public static string DetailListAction = "ListByMaster";
        public static string DraftsAction = "GetDrafts";
        public static string GetModelAction = "GetModel";
        public static string MIMEImage = "image/*";
        public static string MIMEImageFormat = "image/{0}";
        public static string MIMEVideo = "video/*";
        public static string MIMEAudio = "audio/*";
        public static string MaxFileSize = "2MB";
        public const string DefaultImageExtension = "jpeg";
        public const string AcceptedImageExtensions = ".gif,.jpeg,.jpg,.png";
        public const string AcceptedAttachmentExtensions = ".pdf,.jpeg,.jpg,.png";
        public const string DefaultUiGridDefinitionAction = "UiGridDefinition";
        public const int MaxFileSizeNumber = 2;
        public const string ActionColumnName = "_Actions_";
        public const string LegendPattern = "{0}Legend";
        public const string TitlePattern = "{0}s";
        public static Guid PublicStaffGuid = new Guid("45BCCE22-1A7A-40A8-9EF3-EF3A6CE113C2");

        public const string HttpRequestHeaderKeysAcceptLanguage = "Accept-Language";
        public const string HttpRequestCurrentLanguage = "CurrentLanguage";
        public const string DefaultLanguage = "en";
        public const string DefaultAcceptedLanguages = "en,ar";
        public const string DefaultCultureName = "en-US";
    }
}
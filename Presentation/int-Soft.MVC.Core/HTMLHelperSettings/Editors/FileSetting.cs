using intSoft.MVC.Core.Common;
using intSoft.MVC.Core.Enumerations;
using intSoft.MVC.Core.Helpers;

namespace intSoft.MVC.Core.HTMLHelperSettings
{
    /// <summary>
    /// use this class to upload whatever file you want, by default this class let the user to upload 
    /// images with 2MB size maximum, however, you can configure that to accept other file extensions with greater size.
    /// </summary>
    public class FileSetting : EditorSettingBase
    {
        public FileSetting()
        {
            AcceptedFileExtension = DefaultValuesBase.MIMEImage;
            MaxSize = DefaultValuesBase.MaxFileSize;
            DataType = DataDisplayType.Photo;
            SelectFileText = "SelectFileText";
            ChangeFileText = "ChangeFileText";
            RemoveText = "RemoveText";
        }

        public string AcceptedFileExtension { get; set; }
        public string MaxSize { get; set; }
        public string SelectFileText { get; set; }
        public string ChangeFileText { get; set; }
        public string RemoveText    { get; set; }
    }
}
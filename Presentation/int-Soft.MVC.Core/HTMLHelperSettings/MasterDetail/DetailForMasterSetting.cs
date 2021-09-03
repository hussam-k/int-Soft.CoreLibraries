using System;

namespace intSoft.MVC.Core.HTMLHelperSettings.MasterDetail
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DetailForMasterSetting : SettingsBase
    {
        public DetailForMasterSetting(string masterPrimaryKeyPropertyName, string detailForignKeyPropertyName)
        {
            MasterPrimaryKeyPropertyName = masterPrimaryKeyPropertyName;
            DetailForignKeyPropertyName = detailForignKeyPropertyName;
        }


        #region Properties

        /// <summary>
        /// The name of the primary key property in the master wrapper
        /// </summary>
        public string MasterPrimaryKeyPropertyName { get; set; }

        /// <summary>
        /// The name of the forign key property in the detail wrapper
        /// </summary>
        public string DetailForignKeyPropertyName { get; set; }


        #endregion
    }
}
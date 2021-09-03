//using System;
//using System.ComponentModel.DataAnnotations;
//using System.IO;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Routing;
//using System.Web.UI.WebControls;
//using intSoft.MVC.Core.Attributes;
//using intSoft.MVC.Core.DefaultTemplates;
//using intSoft.MVC.Core.Enumerations;
//using intSoft.MVC.Core.Helpers;
//using intSoft.MVC.Core.HTMLHelpers.Base;
//using intSoft.MVC.Core.HTMLHelpers.Editors;
//using intSoft.MVC.Core.HTMLHelperSettings;
//using intSoft.MVC.Core.HTMLHelperSettings.Editors;
//using intSoft.Res.DisplayNames;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace int_Soft.MVC.Core.Tests
//{
//    [TestClass]
//    public class UnitTest1
//    {
//        [TestMethod]
//        public void TestTeamplateReader()
//        {
//            var buttonTemplate = HTMLTemplateProvider.GetTemplate(HTMLTemplates.Button);
//            var expected = "<button type=\"{3}\" class=\"{0}\" ng-click=\"{1}()\">{2}</button>";
//            Assert.AreEqual(expected, buttonTemplate);
//        }
//        //[TestMethod]
//        //public void TestTeacherWrapperEditor()
//        //{
//        //    var editor = CreateHtmlHelper(null).IntSoft().EditorForModel();
//        //    var expected = "<button type=\"{3}\" class=\"{0}\" ng-click=\"{1}()\">{2}</button>";
//        //    Assert.AreEqual(expected, editor);
//        //}

//        public class RegisterView : IView
//        {
//            public void Render(ViewContext viewContext, TextWriter writer)
//            {
//                throw new NotImplementedException();
//            }
//        }
//        public HtmlHelper<TeacherWrapper> CreateHtmlHelper(ViewDataDictionary viewData)
//        {
//            var sw = new StringWriter();
//            var rd = new RouteData();
//            var tc = new AccountController();
//            var td = new TempDataDictionary();
//            var tv = new RegisterView();
//            var req = new HttpRequest("", "http://localhost/", "");
//            var res = new HttpResponse(sw);
//            var hc = new HttpContext(req, res);
//            var hcw = new HttpContextWrapper(hc);
//            var rc = new RequestContext(hcw, rd);
//            var cc = new ControllerContext(rc, tc);
//            var vc = new ViewContext(cc, tv, new ViewDataDictionary(), td, sw);

//            return new HtmlHelper<TeacherWrapper>(vc,new ViewDataContainer() );
//        }
//    }

//    public class ViewDataContainer : IViewDataContainer
//    {
//        public ViewDataContainer()
//        {
//            ViewData = new ViewDataDictionary(new TeacherWrapper());
//        }
//        public ViewDataDictionary ViewData { get; set; }
//    }

//    public class AccountController: ControllerBase
//    {
//        protected override void ExecuteCore()
//        {
//            throw new NotImplementedException();
//        }
//    }

//    [FormSetting(typeof(AccountController), SubmitClientAction = "accountNext", RequireCaptcha = true,
//        WithContainer = false, Title = "MentorRegistrationTitle", Legend = "MentorRegistrationLegend")]
//    public class TeacherWrapper
//    {
//        #region Constructors

//        #endregion

//        #region Properties

//        [DisplayAt(DisplayAt = DisplayAt.Editor, IsHiddenInput = true)]
//        public Guid Id { get; set; }

//        [Display(Name = "Email", ResourceType = typeof(DisplayNames))]
//        [TextBoxSetting(DataType = DataDisplayType.EmailAddress,
//                    BlurExpression = "checkEmailAvailability(currentModel.email)")]
//        [Required(ErrorMessageResourceType = typeof(DisplayNames), ErrorMessageResourceName = "RequiredFieldMessage")]
//        [IntSoftCustomValidation("duplicated", ErrorMessageResourceType = typeof(DisplayNames),
//                    ErrorMessageResourceName = "DuplicatedEmail")]
//        public string Email { get; set; }

//        [Display(Name = "ConfirmEmail", ResourceType = typeof(DisplayNames))]
//        [TextBoxSetting(DataType = DataDisplayType.EmailAddress)]
//        [System.ComponentModel.DataAnnotations.Compare("Email")]
//        [Required(ErrorMessageResourceType = typeof(DisplayNames), ErrorMessageResourceName = "RequiredFieldMessage")]
//        [DisplayAt(DisplayAt = DisplayAt.Editor)]
//        public string ConfirmEmail { get; set; }

//        [DisplayAt(DisplayAt = DisplayAt.None)]
//        public bool EmailConfirmed
//        {
//            get;
//            set;
//        }

//        [Display(ResourceType = typeof(DisplayNames), Name = "Password")]
//        [TextBoxSetting(DataType = DataDisplayType.Password,
//            BlurExpression = "checkPasswordAvailability(currentModel.password)",
//            ResourceType = typeof(DisplayNames),
//            TooltipText = "PasswordToolTip")]
//        [Required(ErrorMessageResourceType = typeof(DisplayNames), ErrorMessageResourceName = "RequiredFieldMessage")]
//        [IntSoftCustomValidation("invalidPassword", ErrorMessageResourceType = typeof(DisplayNames),
//            ErrorMessageResourceName = "InvalidPassword")]
//        [DisplayAt(DisplayAt = DisplayAt.Editor)]
//        public string Password { get; set; }

//        [Display(ResourceType = typeof(DisplayNames), Name = "ConfirmPassword")]
//        [TextBoxSetting(DataType = DataDisplayType.Password)]
//        [Required(ErrorMessageResourceType = typeof(DisplayNames), ErrorMessageResourceName = "RequiredFieldMessage")]
//        [System.ComponentModel.DataAnnotations.Compare("Password")]
//        [DisplayAt(DisplayAt = DisplayAt.Editor)]
//        public string ConfirmPassword { get; set; }

//        [Display(ResourceType = typeof(DisplayNames), Name = "FullName")]
//        [Required(ErrorMessageResourceType = typeof(DisplayNames), ErrorMessageResourceName = "RequiredFieldMessage")]
//        [TextBoxSetting]
//        [DisplayAt(DisplayAt = (DisplayAt)15)]
//        public string FullName
//        {
//            get;
//            set;
//        }

//        [Display(ResourceType = typeof(DisplayNames), Name = "Gender")]
//        [ComboBoxSettings(typeof(GenderEnumSelectListProvider), PlaceHolder = "SelectGenderType")]
//        [Required(ErrorMessageResourceType = typeof(DisplayNames), ErrorMessageResourceName = "RequiredFieldMessage")]
//        public GenderType Gender
//        {
//            get;
//            set;
//        }


//        [Display(ResourceType = typeof(DisplayNames), Name = "DateOfBirth")]
//        [DateTimePickerSetting(UseNestedComboBoxes = true, MaxYear = 2002, MinYear = 1940)]
//        [Required(ErrorMessageResourceType = typeof(DisplayNames), ErrorMessageResourceName = "RequiredFieldMessage")]
//        public DateTime DateOfBirth
//        {
//            get;
//            set;
//        }


//        [DisplayAt(DisplayAt = DisplayAt.Editor, IsHiddenInput = true)]
//        public string PasswordHash
//        {
//            get;
//            set;
//        }

//        [DisplayAt(DisplayAt = DisplayAt.Editor, IsHiddenInput = true)]
//        public string SecurityStamp
//        {
//            get;
//            set;
//        }

//        [Display(ResourceType = typeof(DisplayNames), Name = "PhoneNumber")]
//        [TextBoxSetting(DataType = DataDisplayType.Text, Mask = "+201000000000", PlaceHolder = "+201000000000")]
//        [Required(ErrorMessageResourceType = typeof(DisplayNames), ErrorMessageResourceName = "RequiredFieldMessage")]
//        public string PhoneNumber
//        {
//            get;
//            set;
//        }

//        [DisplayAt(DisplayAt = DisplayAt.Editor, IsHiddenInput = true)]
//        public bool PhoneNumberConfirmed
//        {
//            get;
//            set;
//        }

//        [DisplayAt(DisplayAt = DisplayAt.Editor, IsHiddenInput = true)]
//        public bool TwoFactorEnabled
//        {
//            get;
//            set;
//        }

//        [DisplayAt(DisplayAt = DisplayAt.Editor, IsHiddenInput = true)]
//        public DateTime? LockoutEndDateUtc
//        {
//            get;
//            set;
//        }

//        [DisplayAt(DisplayAt = DisplayAt.Editor, IsHiddenInput = true)]
//        public bool LockoutEnabled
//        {
//            get;
//            set;
//        }

//        [DisplayAt(DisplayAt = DisplayAt.Editor, IsHiddenInput = true)]
//        public int AccessFailedCount
//        {
//            get;
//            set;
//        }

//        [DisplayAt(DisplayAt = DisplayAt.Editor, IsHiddenInput = true)]
//        public string UserName
//        {
//            get;
//            set;
//        }


//        [Display(ResourceType = typeof(DisplayNames), Name = "PricePerHour")]
//        [DisplayAt(DisplayAt = (DisplayAt)15)]
//        [TextBoxSetting(DataType = DataDisplayType.Text, Mask = "000000")]
//        public float? PricePerHour
//        {
//            get;
//            set;
//        }

//        public string PreferredLanguage
//        {
//            get;
//            set;
//        }

//        [DisplayAt(DisplayAt = DisplayAt.Editor, IsHiddenInput = true)]
//        public Guid? InvitedById
//        {
//            get;
//            set;
//        }

//        [Display(ResourceType = typeof(DisplayNames), Name = "Description")]
//        [Required(ErrorMessageResourceType = typeof(DisplayNames), ErrorMessageResourceName = "RequiredFieldMessage")]
//        [MultilineTextBoxSetting]
//        [DisplayAt(DisplayAt = (DisplayAt)15)]
//        public string Description
//        {
//            get;
//            set;
//        }

//        [Display(ResourceType = typeof(DisplayNames), Name = "InvitationCode")]
//        [TextBoxSetting(BlurExpression = "checkInvitationCodeValidity(currentModel.invitationCode)", ResourceType = typeof(DisplayNames), TooltipText = "InvitationCodeDescription")]
//        [IntSoftCustomValidation("invalidInvitationCode", ErrorMessageResourceType = typeof(DisplayNames), ErrorMessageResourceName = "InvalidInvitationCode")]
//        [DisplayAt(DisplayAt = DisplayAt.Editor)]
//        public string InvitationCode
//        {
//            get;
//            set;
//        }

//        [DisplayAt(IsHiddenInput = true)]
//        public DateTime CreationDate
//        {
//            get;
//            set;
//        }


//        public string PhoneVerificationCode { get; set; }

//        #endregion
//    }

//    public enum GenderType : byte
//    {
//        Male = 0,
//        Female = 1
//    }

//    public class GenderEnumSelectListProvider : EnumSelectListProvider<GenderType>
//    {

//    }
//}

namespace intSoft.MVC.Core.HTMLHelpers.Base
{
    public static class IntSoftHTMLHelperExtensions
    {
        public static bool ControllerHasAction(this IntSoftExtension herlper, string actionName)
        {
            var controller = herlper.HtmlHelper.ViewContext.Controller;
            return controller
                .GetType()
                .GetMethod(actionName) != null;
        }

        public static string ControllerName(this IntSoftExtension helper)
        {
            return helper.HtmlHelper.ViewContext.RouteData.GetRequiredString("controller");
        }

        public static string ActionName(this IntSoftExtension helper)
        {
            return helper.HtmlHelper.ViewContext.RouteData.GetRequiredString("action");
        }
    }
}
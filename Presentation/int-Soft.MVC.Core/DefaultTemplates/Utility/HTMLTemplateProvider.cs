using System.IO;
using System.Reflection;
using System.Web;

namespace intSoft.MVC.Core.DefaultTemplates
{
    public static class HTMLTemplateProvider
    {
        private static string TemplateDirectory = "DefaultTemplates";
        private static string AssemblyName = "intSoft.MVC.Core";
        //public static string GetTemplate(string templateFileName)
        //{
        //    var path = string.Format("{0}{1}/{2}", HttpRuntime.BinDirectory, TemplateDirectory, templateFileName);
        //    return File.ReadAllText(path);
        //}


        //private string _templateProjectPath;
        //public string TemplateProjectPath
        //{
        //    get
        //    {
        //        return string.IsNullOrEmpty(_templateProjectPath)
        //            ? MisApplicationConfig.Current.TemplateProjectPath
        //            : _templateProjectPath;
        //    }
        //    set { _templateProjectPath = value; }
        //}

        public static string GetTemplate(string templateFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(string.Format("{0}.{1}.{2}", 
                AssemblyName, TemplateDirectory, templateFileName));
            var sr = new StreamReader(stream);
            var result = sr.ReadToEnd();
            return result;
        }
    }
}
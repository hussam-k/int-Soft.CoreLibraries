using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using IntSoft.DAL.Common;
using intSoft.MVC.Core.ActionResults;
using intSoft.MVC.Core.Bundles;
using intSoft.MVC.Core.ExceptionHandling;
using intSoft.MVC.Core.StructureMap.Base;
using intSoft.MVC.Core.StructureMap.Registries;
using intSoft.MVC.Core.Tasks;
using intSoft.Res.Messages;
using StructureMap;

namespace intSoft.MVC.Core.HTTPApplication
{
    public abstract class HttpApplicationBase<TApplicationConfiguration> : HttpApplication
        where TApplicationConfiguration : ApplicationConfiguration, new()
    {
        protected HttpApplicationBase()
        {
            Configuration = new TApplicationConfiguration();
        }

        public static StructureMapDependencyResolver StructureMapResolver { get; set; }

        public IContainer ApplicationContainer { get; set; }
        public TApplicationConfiguration Configuration { get; set; }

        protected virtual void Application_Start()
        {
            ApplicationContainer = new Container();
            ConfigureContainer();
            SetResolver();
            RunTasks();
            ConfigureSqlExceptionHandler(ApplicationContainer.GetInstance<SqlExceptionHandler>());
            RegisterFrameworkBundle();
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
        }

        protected virtual void RunTasks()
        {
            foreach (var task in ApplicationContainer.GetAllInstances<IRunAtInit>())
                task.Execute();

            foreach (var task in ApplicationContainer.GetAllInstances<IRunAtStartup>())
                task.Execute();
        }

        protected virtual void SetResolver()
        {
            StructureMapResolver = new StructureMapDependencyResolver(ApplicationContainer);
            DependencyResolver.SetResolver(StructureMapResolver);
        }

        protected virtual void ConfigureContainer()
        {
            ApplicationContainer.Configure(cfg =>
            {
                cfg.AddRegistry(new StandardRegistry());
                cfg.AddRegistry(new ControllerRegistry<TApplicationConfiguration>(ApplicationContainer));
                cfg.AddRegistry(new MvcRegistry());
                cfg.AddRegistry(new TaskRegistry<TApplicationConfiguration>(ApplicationContainer));
                cfg.AddRegistry(new CoreLibsRegistry());
                cfg.AddRegistry(new ActionFilterRegistry<TApplicationConfiguration>(() => ApplicationContainer));
            });

            ApplicationContainer.Configure(
                cfg =>
                    cfg.For<IConfiguration>().Use(Configuration));
        }
        
        public virtual void Application_BeginRequest()
        {
            StructureMapResolver.CreateNestedContainer();
            foreach (var task in StructureMapResolver.CurrentNestedContainer.GetAllInstances<IRunOnEachRequest>())
                task.Execute();
        }

        public virtual void Application_EndRequset()
        {
            try
            {
                foreach (
                    var task in StructureMapResolver.CurrentNestedContainer.GetAllInstances<IRunAtAfterEachRequest>())
                    task.Execute();
            }
            finally
            {
                StructureMapResolver.DisposeNestedContainer();
            }
        }

        public virtual void Application_Error()
        {
            foreach (var task in StructureMapResolver.CurrentNestedContainer.GetAllInstances<IRunOnErrors>())
                task.Execute();

            HandleError();
        }

        protected virtual void HandleError()
        {
            var exception = Server.GetLastError();
            Server.ClearError();

            var httpContext = HttpContext.Current;
            if (httpContext == null) return;

            var mvcHandler = httpContext.CurrentHandler as MvcHandler;
            if (mvcHandler == null) return;

            var requestContext = mvcHandler.RequestContext;
            httpContext.Response.Clear();

            var sqlExceptionHandler = StructureMapResolver.CurrentNestedContainer.GetInstance<SqlExceptionHandler>();

            if (requestContext.HttpContext.Request.IsAjaxRequest())
            {
                var result = new StandardJsonActionResult();
                var errorMessage = exception.Message;

                if (sqlExceptionHandler != null)
                    sqlExceptionHandler.TryHandleException(exception, ref errorMessage);

                result.AddError(errorMessage);

                result.ExecuteResult(new ControllerContext
                {
                    HttpContext = DependencyResolver.Current.GetService<HttpContextBase>()
                });
            }
            else
            {
                var viewResult = new ViewResult { ViewName = "Error" };
                var controllerName = requestContext.RouteData.GetRequiredString("controller");
                var factory = ControllerBuilder.Current.GetControllerFactory();
                var controller = factory.CreateController(requestContext, controllerName);

                viewResult.ExecuteResult(new ControllerContext(requestContext, (ControllerBase)controller));
            }
        }

        protected virtual void ConfigureSqlExceptionHandler(SqlExceptionHandler sqlExceptionHandler)
        {
            sqlExceptionHandler.MessagesResourceType = typeof (Messages);
        }

        public virtual void Session_Start(object sender, EventArgs e)
        {
            Session["init"] = 0;
        }
        
        protected virtual void RegisterFrameworkBundle()
        {
            var bundleTable = BundleTable.Bundles;
            bundleTable.UseCdn = HttpContext.Current.IsDebuggingEnabled;
            BundleTable.EnableOptimizations = true;
            var bundleJsFileProviders =
                DependencyResolver.Current.GetServices<IBundleJsFileProvider>().OrderBy(x => x.Order);
            var bundleStyleFileProviders =
                DependencyResolver.Current.GetServices<IBundleStyleFileProvider>().OrderBy(x => x.Order);

            if (HttpContext.Current.IsDebuggingEnabled)
            {
                foreach (var provider in bundleJsFileProviders)
                {
                    foreach (var file in provider.JsFiles)
                    {
                        var fileName = file.Split('/').Last();
                        bundleTable.Add(new ScriptBundle(string.Format("~/{0}", fileName), provider.BasePath + file));
                    }
                }

                foreach (var provider in bundleStyleFileProviders)
                {
                    foreach (var file in provider.StyleFiles)
                    {
                        var fileName = file.Split('/').Last();
                        bundleTable.Add(new StyleBundle(string.Format("~/{0}", fileName), provider.BasePath + file));
                    }
                }
            }
            else
            {
                var scriptBundle = new ScriptBundle("~/intSoft/scripts");
                foreach (var provider in bundleJsFileProviders)
                {
                    foreach (var file in provider.JsFiles)
                    {
                        scriptBundle.Include(provider.BasePath + file);
                    }
                }

                var styleBundle = new StyleBundle("~/intSoft/styles");
                foreach (var provider in bundleStyleFileProviders)
                {
                    foreach (var file in provider.StyleFiles)
                    {
                        styleBundle.Include(provider.BasePath + file);
                    }
                }

                bundleTable.Add(scriptBundle);
                bundleTable.Add(styleBundle);
            }
        }
    }
}
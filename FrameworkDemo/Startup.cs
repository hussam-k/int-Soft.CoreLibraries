using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FrameworkDemo.Startup))]
namespace FrameworkDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

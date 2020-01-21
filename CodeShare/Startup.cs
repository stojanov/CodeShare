using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodeShare.Startup))]
namespace CodeShare
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

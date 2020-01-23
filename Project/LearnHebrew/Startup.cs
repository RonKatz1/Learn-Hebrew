using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LearnHebrew.Startup))]
namespace LearnHebrew
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

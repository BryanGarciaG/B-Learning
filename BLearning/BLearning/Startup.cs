using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BLearning.Startup))]
namespace BLearning
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}

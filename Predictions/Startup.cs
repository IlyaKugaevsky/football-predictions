using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Predictions.Startup))]
namespace Predictions
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

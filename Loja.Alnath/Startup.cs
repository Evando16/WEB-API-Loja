using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Loja.Alnath.Startup))]

namespace Loja.Alnath
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

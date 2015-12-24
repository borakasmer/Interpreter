using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RomanNumeralsConverter.Startup))]
namespace RomanNumeralsConverter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

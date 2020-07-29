using Microsoft.AspNetCore.Hosting;

namespace echobot
{
    /// <summary>
    /// The startup class of the webapp.
    /// </summary>
    public class Startup : Framework.Startup
    {
        public Startup(IWebHostEnvironment env) : base(env, true)
        {
        }
    }
}

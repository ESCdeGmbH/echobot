using Framework.Misc;
using Microsoft.Extensions.Configuration;

namespace echobot
{
    /// <summary>
    /// The bot services used in this echo bot.
    /// </summary>
    public class BotServices : Framework.BotServices
    {
        /// <summary>
        /// Create the services by configuration.
        /// </summary>
        /// <param name="configuration">the configuration</param>
        public BotServices(IConfiguration configuration) : base(configuration, Language.English)
        {
        }
    }
}

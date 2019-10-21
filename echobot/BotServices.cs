using Framework.Misc;
using Microsoft.Extensions.Configuration;

namespace echobot
{
    public class BotServices : Framework.BotServices
    {
        public BotServices(IConfiguration configuration) : base(configuration, Language.English)
        {
        }
    }
}

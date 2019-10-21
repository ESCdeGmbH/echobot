using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace echobot.Controller
{
    [Route("api/messages")]
    [ApiController]
    public class BotController : Framework.Controller.BotController<Bot>
    {
        public BotController(IBotFrameworkHttpAdapter adapter, IConfiguration config, ConversationState state, ILoggerFactory loggerFactory) : base(adapter, config, state, loggerFactory)
        {
        }

        protected override Bot CreateBot() => new Bot(_config, _state, _loggerFactory);
    }
}

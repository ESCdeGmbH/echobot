using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace echobot.Controller
{
    /// <summary>
    /// The controller which handles the messages between users and bot instances.
    /// </summary>
    [Route("api/messages")]
    [ApiController]
    public class BotController : Framework.Controller.BotController<Bot>
    {
        /// <summary>
        /// Create a controller.
        /// </summary>
        /// <param name="adapter">The connector between BotFramework and bot.</param>
        /// <param name="config">The configuration.</param>
        /// <param name="state">The conversational state for the dialog framework.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public BotController(IBotFrameworkHttpAdapter adapter, IConfiguration config, ConversationState state, ILoggerFactory loggerFactory) : base(adapter, config, state, loggerFactory)
        {
        }

        protected override Bot CreateBot() => new Bot(_config, _state, _loggerFactory);
    }
}

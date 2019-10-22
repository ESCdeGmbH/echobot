using echobot.Dialogs;
using echobot.Dialogs.SmallTalk;
using Framework.Dialogs;
using Framework.Dialogs.Smalltalk;
using Framework.Luis;
using Framework.Misc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace echobot
{
    /// <summary>
    /// This is the main class of the bot. It defines the mappings from intents to dialogs.
    /// </summary>
    public class Bot : Framework.Bot<BotServices, IBot4Dialog, BaseDialog<IBot4Dialog, BotServices>>
    {
        // The location of the LUIS Service Definition for classification.
        private static readonly string LUISConfig = RootPath.GetRootPath("echobot", "luis.json");

        public Bot(IConfiguration config, ConversationState state, ILoggerFactory loggerFactory) : base(state, new BotServices(config), loggerFactory, null)
        {
            LuisServiceDefinition lsd = JsonConvert.DeserializeObject<LuisServiceDefinition>(File.ReadAllText(LUISConfig));
            // Use LSD and no spell checking
            _recognizer = new CombinedLuisRecognizer(lsd, false);
        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            await _recognizer.Recognize(turnContext, cancellationToken);
            await HandleDialog(turnContext, cancellationToken);
        }

        protected override void LoadDialogs(out DialogSet dialogs, out List<BaseDialog<IBot4Dialog, BotServices>> instances)
        {
            // All instances of custom dialogs.
            instances = new List<BaseDialog<IBot4Dialog, BotServices>>() {
                new NoneDialog(BotServices, this),
                new EchoDialog(BotServices, this),

                // Smalltalk
                new SingleStepSmalltalk<IBot4Dialog,BotServices>(BotServices, this, nameof(SingleStepSmalltalk<IBot4Dialog,BotServices>), RootPath.GetRootPath("echobot", "Dialogs", "SmallTalk-Data")),
                new GreetingDialog(BotServices, this),
             };

            // All dialogs in a DialogSet.
            var set = new DialogSet(State.CreateProperty<DialogState>("DialogState"));
            instances.ForEach(d => set.Add(d));
            dialogs = set;
        }

        protected override Dictionary<string, Handler> LoadLuisHandlers() => new Dictionary<string, Handler>
        {
                // Mappings from intent (lower case) to dialogs. As ID we've used the names of the classes
                { "None".ToLower(), StartDialog(nameof(NoneDialog)) },
                { "Echo".ToLower(), StartDialog(nameof(EchoDialog)) },
                // Special Smalltalks
                { "ST_Greeting".ToLower(), StartDialog(nameof(GreetingDialog)) }
        };

        protected override async Task<bool> SelectTopic(ITurnContext context, CancellationToken cancellationToken)
        {
            if (Result == null)
            {
                // Is initialization ..
                return true;
            }

            // Find TopIntent
            var topIntent = Result?.GetTopScoringIntent();
            // If classification is too bad, set to none.
            topIntent = (topIntent?.score ?? 0) < 0.3 ? ("None", 1) : topIntent;

            bool contains = LuisHandlers.TryGetValue(topIntent?.intent?.ToLower(), out Handler handler);
            if (contains)
            {
                // Default handler
                await handler(context, GetEntities());
                return true;
            }
            else if (topIntent?.intent?.ToLower()?.StartsWith("st_") ?? false)
            {
                // Smalltalk
                await StartDialog(nameof(SingleStepSmalltalk<IBot4Dialog, BotServices>))(context, GetEntities());
                return true;
            }
            else
            {
                await SendMessage($"I did not found anything for {topIntent?.intent}", context);
            }
            return false;
        }
    }
}

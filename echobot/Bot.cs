using echobot.Dialogs;
using echobot.Dialogs.SmallTalk;
using Framework.Classifier;
using Framework.Dialogs;
using Framework.Dialogs.Smalltalk;
using Framework.Misc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private static readonly string LUISConfig = MiscExtensions.LoadEmbeddedResource("echobot.luis.json");

        public Bot(IConfiguration config, ConversationState state, ILoggerFactory loggerFactory) : base(state, new BotServices(config), loggerFactory, null)
        {
            LuisServiceDefinition lsd = JsonConvert.DeserializeObject<LuisServiceDefinition>(LUISConfig);
            // Use LSD and no spell checking
            _recognizer = new LuisClassifier(lsd, false);
            LoadIntentHandlers();
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
                new SingleStepSmalltalk<IBot4Dialog,BotServices>(BotServices, this, nameof(SingleStepSmalltalk<IBot4Dialog,BotServices>), "echobot.Dialogs.SmallTalk_Data"),
                new GreetingDialog(BotServices, this),
             };

            // All dialogs in a DialogSet.
            var set = new DialogSet(State.CreateProperty<DialogState>("DialogState"));
            instances.ForEach(d => set.Add(d));
            dialogs = set;
        }

        private void LoadIntentHandlers()
        {
            var handler = new Dictionary<string, string>
            {
                // Mappings from intent (lower case) to dialogs. As ID we've used the names of the classes
                { "None".ToLower(), nameof(NoneDialog) },
                { "Echo".ToLower(), nameof(EchoDialog) },
                // Special Smalltalks
                { "ST_Greeting".ToLower(), nameof(GreetingDialog) }
            };

            handler.Keys.ToList().ForEach(k => IntentHandler.Add(k, handler[k]));
        }

        protected override async Task<string> ClassifyDialog(ITurnContext context)
        {
            if (Result == null)
            {
                // Is initialization ..
                return null;
            }

            // Find TopIntent
            var topIntent = Result?.GetTopScoringIntent();
            // If classification is too bad, set to none.
            topIntent = (topIntent?.Item2 ?? 0) < 0.3 ? ("None", 1) : topIntent;

            bool contains = IntentHandler.TryGetValue(topIntent?.Item1?.ToLower(), out string handler);
            if (contains)
            {
                // Default handler
                return handler;
            }
            else if (topIntent?.Item1?.ToLower()?.StartsWith("st_") ?? false)
            {
                // Smalltalk
                return nameof(SingleStepSmalltalk<IBot4Dialog, BotServices>);
            }
            else
            {
                await SendMessage($"I did not found anything for {topIntent?.Item1}", context);
            }
            return null;
        }
    }
}

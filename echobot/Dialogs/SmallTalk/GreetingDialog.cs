using Framework.Dialogs;
using Framework.Dialogs.Smalltalk;
using Framework.Misc;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace echobot.Dialogs.SmallTalk
{
    public class GreetingDialog : MultiStepSmallTalkDialog<IBot4Dialog, BotServices>
    {
        public GreetingDialog(BotServices services, IBot4Dialog bot) : base(services, bot, nameof(GreetingDialog), RootPath.GetRootPath("echobot", "Dialogs", "SmallTalk-Data"))
        {
        }

        protected override void AddInitialSteps()
        {
            // Just say hello
            AddStep(SimpleAnswer);
            // Ask for name
            AddStep(Ask4Name);
            // Respond to Name
            AddStep(RespondToName);
        }

        private async Task<DialogTurnResult> Ask4Name(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await TheBot.SendMessage("What is your name?", stepContext.Context);
            // Wait for user input ..
            return new DialogTurnResult(DialogTurnStatus.Waiting);
        }
        private async Task<DialogTurnResult> RespondToName(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Respond with name ..
            await TheBot.SendMessage($"Hello, your name is {stepContext.Context.Activity.Text}", stepContext.Context);
            return await stepContext.NextAsync();
        }

    }
}
